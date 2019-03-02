[<RequireQualifiedAccess>]
module FSharp.Utils.Dictionary

open System.Collections.Generic
open Newtonsoft.Json
open Newtonsoft.Json.Linq

let inline private (|Dict|) (dict: #IDictionary<_, _>) = dict :> IDictionary<_, _>

let empty<'key, 'value when 'key: equality> =
    Dictionary<'key, 'value> ()

let contains key (Dict dict) = dict.ContainsKey key

let add key value (Dict dict) =
    dict.[key] <- value
    dict

let remove key (Dict dict) =
    if not (dict.Remove (key = key)) then
        raise (KeyNotFoundException (sprintf "Key '%A' not found!" key))
    dict

let tryRemove key (Dict dict) =
    dict.Remove(key = key) |> ignore
    dict

let ofList (list: ('key * 'value) list) =
    list
    |> List.fold (fun state (key, value) -> add key value state) (upcast empty)

let toList (Dict dict) =
    (dict.Keys, dict.Values)
    ||> Seq.zip
    |> Seq.toList

let tryFind key (Dict dict) =
    match dict.TryGetValue key with
    | true, value -> Some value
    | _ -> None

let merge (Dict lhs) (Dict rhs) =
    ofList [
        yield! lhs |> Seq.map (|KeyValue|)
        yield! rhs |> Seq.map (|KeyValue|)
    ]

// FIXME: This is a hacky way to do this right now.
let ofObj x =
    let rec run x =
        try
            let dict =
                JsonConvert.SerializeObject x
                |> JsonConvert.DeserializeObject<Dictionary<string, obj>>
                |> (|Dict|)
            dict
            |> toList
            |> List.filter (fun (_, value) -> value :? JObject)
            |> List.iter (fun (key, value) -> dict.[key] <- (Option.get >> box) (run value))
            Some dict
        with _ -> None
    run x

let count (Dict dict) = dict.Count

let exists f (Dict dict) =
    dict
    |> Seq.exists (fun (KeyValue (key, value)) -> f key value)
