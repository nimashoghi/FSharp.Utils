[<RequireQualifiedAccess>]
module FSharp.Utils.ResizeArray

open System

let add (element: 't) (array: 't ResizeArray) =
    array.Add element
    array

let map f (array: 't ResizeArray) =
    array
    |> Seq.map f
    |> ResizeArray

let filter f (array: 't ResizeArray) =
    array
    |> Seq.filter f
    |> ResizeArray

let choose f (array: 't ResizeArray) =
    array
    |> Seq.choose f
    |> ResizeArray

let tryRemove (element: 't) (array: 't ResizeArray) =
    array.Remove element

let remove (element: 't) (array: 't ResizeArray) =
    array.Remove element |> ignore
    array

let removeAll (f: 't -> bool) (array: 't ResizeArray) =
    array.RemoveAll (Predicate<_> f) |> ignore
    array
