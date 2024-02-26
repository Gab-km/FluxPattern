namespace FluxPattern.Logic

type Action =
    | Initial of int
    | Add of int
    | Subtract of int


module ActionCreator =
    [<CompiledName "CreateInitial">]
    let createInitial (value: int) = Initial value
    [<CompiledName "CreateAdd">]
    let createAdd (value: int) = Add value
    [<CompiledName "CreateSub">]
    let createSub (value: int) = Subtract value