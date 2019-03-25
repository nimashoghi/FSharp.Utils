[<RequireQualifiedAccess>]
module FSharp.Utils.SortedSet

open System
open System.Collections.Generic

let length (array: _ SortedSet) = array.Count

let add (element: 't) (array: 't SortedSet) =
    array.Add element |> ignore
    array

let tryAdd (element: 't) (array: 't SortedSet) =
    array.Add element

let map f (array: 't SortedSet) =
    array
    |> Seq.map f
    |> SortedSet

let filter f (array: 't SortedSet) =
    array
    |> Seq.filter f
    |> SortedSet

let mapFilter filterFn mapFn array =
    array
    |> map (
        fun element ->
            if filterFn element
            then mapFn element
            else element
    )

let mapFilterOption f array =
    array
    |> map (
        fun element ->
            match f element with
            | Some value -> value
            | None -> element
    )

let choose f (array: 't SortedSet) =
    array
    |> Seq.choose f
    |> SortedSet

let tryRemove (element: 't) (array: 't SortedSet) =
    if array.Remove element
    then Some array
    else None

let remove (element: 't) (array: 't SortedSet) =
    array.Remove element |> ignore
    array

let removeWhere (f: 't -> bool) (array: 't SortedSet) =
    array.RemoveWhere (Predicate<_> f) |> ignore
    array

let tryRemoveWhere (f: 't -> bool) (array: 't SortedSet) =
    if array.RemoveWhere (Predicate<_> f) > 0
    then Some array
    else None
