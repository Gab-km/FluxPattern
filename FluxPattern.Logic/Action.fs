namespace FluxPattern.Logic

type Action =
    | Add of int
    | Subtract of int
    override this.ToString() =
        match this with
        | Add _ -> "Add"
        | Subtract _ -> "Subtract"


module ActionCreator =
    [<CompiledName "CreateAdd">]
    let createAdd (value: int) = Add value
    [<CompiledName "CreateSub">]
    let createSub (value: int) = Subtract value