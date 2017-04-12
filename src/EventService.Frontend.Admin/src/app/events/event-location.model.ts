export class EventLocation { 
    public id: any;

    public address: string;

    public city: string;

    public province: string;

    public postalCode: string;

    public longitude: string;

    public latitude: string;  

    public fromJSON(data: any): EventLocation {
        let eventLocation = new EventLocation();
        eventLocation.address = data.address;
        eventLocation.city = data.city;
        eventLocation.province = data.province;
        eventLocation.postalCode = data.postalCode;
        eventLocation.longitude = data.longitude;
        eventLocation.latitude = data.latitude;         
        return eventLocation;
    }
}
