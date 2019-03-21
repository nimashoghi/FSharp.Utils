[<RequireQualifiedAccess>]
module FSharp.Utils.Result

let tryGet (x: Result<_, _>) =
    match x with
    | Ok value -> Some value
    | _ -> None

let get x = tryGet x |> Option.get
