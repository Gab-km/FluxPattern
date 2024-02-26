﻿namespace FluxPattern.Logic

module Dispatcher =
    [<CompiledName "Dispatch">]
    let dispatch (action: Action) = Store.reduce action
