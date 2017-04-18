import { Location } from "../locations";
import { EventType } from "./event-type.model";

export class Event { 
    public id:any;

    public name: string;

    public displayName: string;

    public url: string;

    public imageUrl: string;

    public description: string;

    public abstract: string;
    
    public start: string;

    public end: string;

    public eventLocation: Location;

    public eventType: EventType;

    public static fromJSON(data: any): Event {
        let event = new Event();

        event.name = data.name;

        event.displayName = data.displayName;

        event.imageUrl = data.imageUrl;  

        event.description = data.description;

        event.abstract = data.abstract;     

        event.start = data.start;

        event.end = data.end;

        event.eventLocation = Location.fromJSON(data.location);

        event.eventType = EventType.fromJSON(data.eventType);

        return event;
    }
}
