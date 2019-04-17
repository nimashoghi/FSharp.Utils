[<RequireQualifiedAccess>]
module FSharp.Utils.SortedDictionary

open System.Collections.Generic

let add key value (dictionary: SortedDictionary<_, _>) =
    dictionary.[key] <- value
    dictionary

let tryAdd key value (dictionary: SortedDictionary<_, _>) =
    if dictionary.ContainsKey key
    then None
    else
        dictionary.[key] <- value
        Some dictionary

let tryGet key (dictionary: SortedDictionary<_, _>) =
    match dictionary.TryGetValue key with
    | true, value -> Some value
    | _ -> None

let tryRemove key (dictionary: SortedDictionary<_, _>) =
    match dictionary.TryGetValue key with
    | true, value ->
        if dictionary.Remove key
        then Some struct (value, dictionary)
        else None
    | _ -> None

let tryPick f (dictionary: SortedDictionary<_, _>) =
    dictionary
    |> Seq.tryPick (fun (KeyValue (key, value)) -> f key value)

let tryFind f (dictionary: SortedDictionary<_, _>) =
    dictionary
    |> tryPick (
        fun key value ->
            if f key value
            then Some (key, value)
            else None
    )

let map f (dictionary: SortedDictionary<_, _>) =
    dictionary
    |> Seq.map (fun (KeyValue (key, value)) -> key, f key value)
    |> dict
    |> SortedDictionary
