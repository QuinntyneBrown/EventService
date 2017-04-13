import { Location } from "./location.model";
import { EditorComponent } from "../shared";
import { LocationDelete, LocationEdit, LocationAdd } from "./location.actions";

const template = require("./location-edit-embed.component.html");
const styles = require("./location-edit-embed.component.scss");

export class LocationEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "location",
            "location-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.location ? "Edit Location": "Create Location";

        if (this.location) {                
            this._addressInputElement.value = this.location.address;
            this._cityInputElement.value = this.location.city;
            this._provinceInputElement.value = this.location.province;
            this._postalCodeInputElement.value = this.location.postalCode;           
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
        const location = {
            address: this._addressInputElement.value,
            city: this._cityInputElement.value,
            province: this._provinceInputElement.value,
            postalCode: this._postalCodeInputElement.value            
        } as Location;
        
        this.dispatchEvent(new LocationAdd(location));            
    }

    public onDelete() {        
        const location = {
            address: this._addressInputElement.value,
            city: this._cityInputElement.value,
            province: this._provinceInputElement.value,
            postalCode: this._postalCodeInputElement.value
        } as Location;

        this.dispatchEvent(new LocationDelete(location));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "location-id":
                this.locationId = newValue;
                break;
            case "location":
                this.location = JSON.parse(newValue);
                if (this.parentNode) {
                    this._titleElement.textContent = this.locationId ? "Edit Location" : "Create Location";
                    this._addressInputElement.value = this.location.address != undefined ? this.location.address : "";
                    this._cityInputElement.value = this.location.city != undefined ? this.location.city : "";
                    this._provinceInputElement.value = this.location.province != undefined ? this.location.province : "";
                    this._postalCodeInputElement.value = this.location.postalCode != undefined ? this.location.postalCode : "";
                }
                break;
        }           
    }

    public locationId: any;

    public location: Location;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }

    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-location-button") as HTMLElement };

    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-location-button") as HTMLElement };

    private get _addressInputElement(): HTMLInputElement { return this.querySelector(".location-address") as HTMLInputElement; }

    private get _cityInputElement(): HTMLInputElement { return this.querySelector(".location-city") as HTMLInputElement; }

    private get _provinceInputElement(): HTMLInputElement { return this.querySelector(".location-province") as HTMLInputElement; }

    private get _postalCodeInputElement(): HTMLInputElement { return this.querySelector(".location-postal-code") as HTMLInputElement; }
}

customElements.define(`ce-location-edit-embed`,LocationEditEmbedComponent);
