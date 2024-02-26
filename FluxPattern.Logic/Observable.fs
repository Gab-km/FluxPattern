namespace FluxPattern.Logic

type ISubscriber =
    abstract Update: context: string -> unit

type IPublisher =
    abstract Subscribe: s: ISubscriber -> unit
    abstract Unsubscribe: s: ISubscriber -> unit
    abstract NotifySubscribers: unit -> unit
