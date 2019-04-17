module rec FSharp.Utils.Maybe

type MaybeBuilder () =
    member __.Bind (x, f) =
        match x with
        | None -> None
        | Some a -> f a
    member __.Bind (x, f) =
        match x with
        | ValueNone -> None
        | ValueSome a -> f a
    member __.Return x = Some x
    member __.ReturnFrom (x: _ option) = x
    member __.ReturnFrom x =
        match x with
        | ValueNone -> None
        | ValueSome value -> Some value
    member __.Zero () = None


type ValueMaybeBuilder () =
    member __.Bind (x, f) =
        match x with
        | None -> ValueNone
        | Some a -> f a
    member __.Bind (x, f) =
        match x with
        | ValueNone -> ValueNone
        | ValueSome a -> f a
    member __.Return x = ValueSome x
    member __.ReturnFrom (x: _ voption) = x
    member __.ReturnFrom x =
        match x with
        | None -> ValueNone
        | Some value -> ValueSome value
    member __.Zero () = ValueNone

let maybe = MaybeBuilder ()
let vmaybe = ValueMaybeBuilder ()
