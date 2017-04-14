const template = require("./near-by-event.component.html");
const styles = require("./near-by-event.component.scss");

export class NearByEventComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes () {
        return [
            "event",
            "order-index"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        if (this.event) {
            this._orderIndexElement.innerHTML = `${this.orderIndex}`; 
            this._nameElement.innerHTML = `${this.event.name}`;
            this._distanceElement.innerHTML = `${this.event.eventLocation.distance}`;
        }
        //this._title.innerHTML = `${this.orderIndex} ${this.event.name}, ${this.event.eventLocation.address}, ${this.event.eventLocation.distance}`;
    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    private event: any;

    private orderIndex: any;

    
    private get _orderIndexElement(): HTMLElement { return this.querySelector(".near-by-event-order-index") as HTMLElement; }

    private get _nameElement(): HTMLElement { return this.querySelector(".near-by-event-name") as HTMLElement; }

    private get _distanceElement(): HTMLElement { return this.querySelector(".near-by-event-distance") as HTMLElement; }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "event":
                this.event = JSON.parse(newValue);
                
                if (this.parentNode)
                    this._bind();

                break;

            case "order-index":
                this.orderIndex = newValue;
                break;
        }
    }
}

customElements.define(`ce-near-by-event`,NearByEventComponent);
