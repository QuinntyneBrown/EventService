export class Event { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Event {
        let event = new Event();
        event.name = data.name;
        return event;
    }
}
