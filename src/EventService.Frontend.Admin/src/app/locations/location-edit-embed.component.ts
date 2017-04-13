import { Location } from "./location.model";
import { EditorComponent } from "../shared";

const template = require("./location-edit-embed.component.html");
const styles = require("./location-edit-embed.component.scss");

export class LocationEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes() {
        return [
            "location",
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
    }
    
    private async _bind() {        
        if (this.location) {
            this._addressInputElement.value = this.location.address;
            this._cityInputElement.value = this.location.city;
            this._provinceInputElement.value = this.location.province;
            this._postalCodeInputElement.value = this.location.postalCode;
        }
    }
    
    public get value(): Location {
        return {
            address: this._addressInputElement.value,
            city: this._cityInputElement.value,
            province: this._provinceInputElement.value,
            postalCode: this._postalCodeInputElement.value
        } as Location;
    }

    public set value(location: Location) {        
        this._addressInputElement.value = location.address != undefined ? location.address : "";
        this._cityInputElement.value = location.city != undefined ? location.city : "";
        this._provinceInputElement.value = location.province != undefined ? location.province : "";
        this._postalCodeInputElement.value = location.postalCode != undefined ? location.postalCode : "";        
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "location":
                this.location = JSON.parse(newValue);
                if (this.parentNode) {
                    alert("?");
                    this._addressInputElement.value = this.location.address != undefined ? this.location.address : "";
                    this._cityInputElement.value = this.location.city != undefined ? this.location.city : "";
                    this._provinceInputElement.value = this.location.province != undefined ? this.location.province : "";
                    this._postalCodeInputElement.value = this.location.postalCode != undefined ? this.location.postalCode : "";
                }
                break;
        }           
    }
    
    public location: Location;

    private get _addressInputElement(): HTMLInputElement { return this.querySelector(".location-address") as HTMLInputElement; }

    private get _cityInputElement(): HTMLInputElement { return this.querySelector(".location-city") as HTMLInputElement; }

    private get _provinceInputElement(): HTMLInputElement { return this.querySelector(".location-province") as HTMLInputElement; }

    private get _postalCodeInputElement(): HTMLInputElement { return this.querySelector(".location-postal-code") as HTMLInputElement; }
}

customElements.define(`ce-location-edit-embed`,LocationEditEmbedComponent);
