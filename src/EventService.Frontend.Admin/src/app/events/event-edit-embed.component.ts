import { Event } from "./event.model";
import { EditorComponent } from "../shared";
import {  EventDelete, EventEdit, EventAdd } from "./event.actions";

const template = require("./event-edit-embed.component.html");
const styles = require("./event-edit-embed.component.scss");

export class EventEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "event",
            "event-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.event ? "Edit Event": "Create Event";

        if (this.event) {                
            this._nameInputElement.value = this.event.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const event = {
            id: this.event != null ? this.event.id : null,
            name: this._nameInputElement.value
        } as Event;
        
        this.dispatchEvent(new EventAdd(event));            
    }

    public onDelete() {        
        const event = {
            id: this.event != null ? this.event.id : null,
            name: this._nameInputElement.value
        } as Event;

        this.dispatchEvent(new EventDelete(event));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "event-id":
                this.eventId = newValue;
                break;
            case "event":
                this.event = JSON.parse(newValue);
                if (this.parentNode) {
                    this.eventId = this.event.id;
                    this._nameInputElement.value = this.event.name != undefined ? this.event.name : "";
                    this._titleElement.textContent = this.eventId ? "Edit Event" : "Create Event";
                }
                break;
        }           
    }

    public eventId: any;
    public event: Event;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".event-name") as HTMLInputElement;}
}

customElements.define(`ce-event-edit-embed`,EventEditEmbedComponent);
