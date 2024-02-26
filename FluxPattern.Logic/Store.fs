namespace FluxPattern.Logic

type internal IStore =
    abstract State: int with get, set
    abstract Reduce: action: Action -> unit

type internal StoreClass() =
    let state = ref 0
    let mutable subscribers: ISubscriber list = []

    interface IStore with
        member this.State
            with get () = state.Value
            and set (value) =
                if (state.Value <> value) then
                    state.Value <- value
                    (this :> IPublisher).NotifySubscribers()

        member this.Reduce(action: Action) =
            let store :IStore = this
            match action with
            | Initial i -> store.State <- i
            | Add a -> store.State <- store.State + a
            | Subtract s -> store.State <- store.State - s

    interface IPublisher with
        member this.NotifySubscribers() =
            subscribers
            |> List.iter (fun sub -> sub.Update("State"))

        member this.Subscribe(s: ISubscriber) = subscribers <- s :: subscribers

        member this.Unsubscribe(s: ISubscriber) =
            let filtered =
                subscribers
                |> List.fold (fun acc elem -> if elem = s then acc else elem :: acc) []

            subscribers <- List.rev filtered

module Store =
    let private singleton = StoreClass()

    let private store: IStore = singleton
    let private publisher: IPublisher = singleton

    let internal reduce = store.Reduce

    [<CompiledName "GetState">]
    let state () = store.State

    [<CompiledName "Subscribe">]
    let subscribe (s: ISubscriber) = publisher.Subscribe s

    [<CompiledName "Unsubscribe">]
    let unsubscribe (s: ISubscriber) = publisher.Unsubscribe s
