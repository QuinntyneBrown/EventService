export class Location { 

    public address: any;

    public city: any;

    public province: any;

    public postalCode: any;

    public longitude: any;

    public latitude: any;

    public static fromJSON(data: any): Location {
        let location = new Location();
        
        return location;
    }
}
