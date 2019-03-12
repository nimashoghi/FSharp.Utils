[<RequireQualifiedAccess>]
module FSharp.Utils.Option

open System.Threading.Tasks

open FSharp.Utils.Tasks

let getOrRaiseWith (f: unit -> #exn) x =
    match x with
    | Some value -> value
    | None -> raise (f ())

let getOrRaise exn x = getOrRaiseWith (fun () -> exn) x

let ofNullObj (x: 'value) =
    box x
    |> Option.ofObj
    |> Option.map unbox<'value>

let toNullObj (x: 'value option) =
    x
    |> Option.map box
    |> Option.toObj
    |> unbox<'value>

let bindTask
    (f: 'value -> 'result option Task)
    (x: 'value option)
    : 'result option Task =
    match x with
    | Some value ->
        task {
            match! f value with
            | Some value -> return Some value
            | None -> return None
        }
    | None -> Task.FromResult None
