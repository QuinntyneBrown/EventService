import { Location } from "../locations";

export class Event { 
    public id:any;

    public name: string;

    public imageUrl: string;

    public description: string;

    public abstract: string;
    
    public start: string;

    public end: string;

    public eventLocation: Location;

    public static fromJSON(data: any): Event {
        let event = new Event();

        event.name = data.name;

        event.imageUrl = data.imageUrl;  

        event.description = data.description;

        event.abstract = data.abstract;     

        event.start = data.start;

        event.end = data.end;

        event.eventLocation = Location.fromJSON(data.location);

        return event;
    }
}
