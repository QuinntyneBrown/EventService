import { Event } from "./event.model";

const template = require("./event-list-embed.component.html");
const styles = require("./event-list-embed.component.scss");

export class EventListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "events"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.events.length; i++) {
            let el = this._document.createElement(`ce-event-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.events[i]));
            this.appendChild(el);
        }    
    }

    events:Array<Event> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "events":
                this.events = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-event-list-embed", EventListEmbedComponent);
