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
            this._imageUrlInputElement.value = this.event.imageUrl;
            this._abstractInputElement.value = this.event.abstract;
            this._descriptionInputElement.value = this.event.description;     
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
            name: this._nameInputElement.value,
            imageUrl: this._imageUrlInputElement.value,
            description: this._descriptionInputElement.value,
            abstract: this._abstractInputElement.value,
            start: this._startInputElement.value,
            end: this._endInputElement.value
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
                    this._imageUrlInputElement.value = this.event.imageUrl != undefined ? this.event.imageUrl : "";
                    this._startInputElement.value = this.event.start != undefined ? this.event.start : "";
                    this._endInputElement.value = this.event.end != undefined ? this.event.end : "";
                    this._abstractInputElement.value = this.event.abstract != undefined ? this.event.abstract : "";
                    this._descriptionInputElement.value = this.event.description != undefined ? this.event.description : "";
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

    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".event-name") as HTMLInputElement; }

    private get _startInputElement(): HTMLInputElement { return this.querySelector(".event-start") as HTMLInputElement; }

    private get _endInputElement(): HTMLInputElement { return this.querySelector(".event-end") as HTMLInputElement; }

    private get _imageUrlInputElement(): HTMLInputElement { return this.querySelector(".event-image-url") as HTMLInputElement; }

    private get _descriptionInputElement(): HTMLInputElement { return this.querySelector(".event-description") as HTMLInputElement; }

    private get _abstractInputElement(): HTMLInputElement { return this.querySelector(".event-abstract") as HTMLInputElement; }

}

customElements.define(`ce-event-edit-embed`,EventEditEmbedComponent);
