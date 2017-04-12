import { ApiService } from "../shared";
import { getCurrentPositionAsync } from "../utilities";

const template = require("./splash.component.html");
const styles = require("./splash.component.scss");

export class SplashComponent extends HTMLElement {
    constructor(
        private _apiService: ApiService = ApiService.Instance
    ) {
        super();
    }

    static get observedAttributes () {
        return [];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        var coordinates = await getCurrentPositionAsync();
        var address = await this._apiService.getAddress({
            longitude: coordinates.longitude,
            latitude: coordinates.latitude
        });
        alert(address);
    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }
}

customElements.define(`ce-splash`,SplashComponent);
