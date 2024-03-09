namespace FluxPattern.Logic

type internal IReducer<'T> =
    abstract Reduce: action: Action -> 'T -> 'T

type internal Reducer() =
    interface IReducer<int> with
        member this.Reduce (action: Action) (value: int) =
            match action with
            | Add a -> value + a
            | Subtract s -> value - s

type internal IStore<'T> =
    inherit IPublisher
    abstract GetState: 'T with get
    abstract Update: action: Action -> unit

type internal StoreClass() =
    let mutable state = 0
    let reducer: IReducer<int> = Reducer()
    let mutable subscribers: ISubscriber list = []

    interface IStore<int> with
        member this.GetState = state

        member this.Update(action: Action) =
            let reduced = reducer.Reduce action state
            if (state <> reduced) then
                state <- reduced
                (this :> IPublisher).NotifySubscribers(action.ToString())

        member this.NotifySubscribers(context: string) =
            subscribers
            |> List.iter (fun sub -> sub.Update(context))

        member this.Subscribe(s: ISubscriber) = subscribers <- s :: subscribers

        member this.Unsubscribe(s: ISubscriber) =
            let filtered =
                subscribers
                |> List.fold (fun acc elem -> if elem = s then acc else elem :: acc) []

            subscribers <- List.rev filtered

module Store =
    let private singleton: IStore<int> = StoreClass()

    let internal update = singleton.Update

    [<CompiledName "GetState">]
    let state () = singleton.GetState

    [<CompiledName "Subscribe">]
    let subscribe (s: ISubscriber) = singleton.Subscribe s

    [<CompiledName "Unsubscribe">]
    let unsubscribe (s: ISubscriber) = singleton.Unsubscribe s
