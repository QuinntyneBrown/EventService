import { Event } from "./event.model";

export const eventActions = {
    ADD: "[Event] Add",
    EDIT: "[Event] Edit",
    DELETE: "[Event] Delete",
    EVENTS_CHANGED: "[Event] Events Changed"
};

export class EventEvent extends CustomEvent {
    constructor(eventName:string, event: Event) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { event }
        });
    }
}

export class EventAdd extends EventEvent {
    constructor(event: Event) {
        super(eventActions.ADD, event);        
    }
}

export class EventEdit extends EventEvent {
    constructor(event: Event) {
        super(eventActions.EDIT, event);
    }
}

export class EventDelete extends EventEvent {
    constructor(event: Event) {
        super(eventActions.DELETE, event);
    }
}

export class EventsChanged extends CustomEvent {
    constructor(events: Array<Event>) {
        super(eventActions.EVENTS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { events }
        });
    }
}
