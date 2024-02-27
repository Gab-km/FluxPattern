namespace FluxPattern.Logic

type internal IReducer<'T> =
    abstract Reduce: action: Action -> 'T -> 'T

type internal Reducer() =
    interface IReducer<int> with
        member this.Reduce (action: Action) (value: int) =
            match action with
            | Initial i -> i
            | Add a -> value + a
            | Subtract s -> value - s

type internal IStore =
    abstract State: int with get, set
    abstract Update: action: Action -> unit

type internal StoreClass() =
    let state = ref 0
    let reducer: IReducer<int> = Reducer()
    let mutable subscribers: ISubscriber list = []

    interface IStore with
        member this.State
            with get () = state.Value
            and set (value) =
                if (state.Value <> value) then
                    state.Value <- value
                    (this :> IPublisher).NotifySubscribers()

        member this.Update(action: Action) =
            let store :IStore = this
            store.State <- reducer.Reduce action state.Value

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

    let internal update = store.Update

    [<CompiledName "GetState">]
    let state () = store.State

    [<CompiledName "Subscribe">]
    let subscribe (s: ISubscriber) = publisher.Subscribe s

    [<CompiledName "Unsubscribe">]
    let unsubscribe (s: ISubscriber) = publisher.Unsubscribe s
