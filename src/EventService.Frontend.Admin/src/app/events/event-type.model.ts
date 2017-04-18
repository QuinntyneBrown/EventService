export class EventType { 
    public id:any;
    public name:string;

    public static fromJSON(data: { name:string }): EventType {
        let eventType = new EventType();
        eventType.name = data.name;
        return eventType;
    }
}
