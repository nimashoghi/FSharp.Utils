[<RequireQualifiedAccess>]
module FSharp.Utils.ValueOption

let ofNullObj x =
    if x = Unchecked.defaultof<_>
    then ValueNone
    else ValueSome x

let toNullObj (x: 'value voption) =
    x
    |> ValueOption.defaultValue Unchecked.defaultof<'value>
