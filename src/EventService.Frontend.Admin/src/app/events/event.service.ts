import { fetch } from "../utilities";
import { Event } from "./event.model";

export class EventService {
    constructor(private _fetch = fetch) { }

    private static _instance: EventService;

    public static get Instance() {
        this._instance = this._instance || new EventService();
        return this._instance;
    }

    public get(): Promise<Array<Event>> {
        return this._fetch({ url: "/api/event/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { events: Array<Event> }).events;
        });
    }

    public getById(id): Promise<Event> {
        return this._fetch({ url: `/api/event/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { event: Event }).event;
        });
    }

    public add(event) {
        return this._fetch({ url: `/api/event/add`, method: "POST", data: { event }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/event/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
