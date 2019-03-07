[<RequireQualifiedAccess>]
module FSharp.Utils.HashSet

open System.Collections.Generic

let inline internal (|HashSet|) (set: _ HashSet) = set

let internal ``do`` (f: _ HashSet -> _) set = (f >> ignore) set; set

let ofSeq (seq: _ seq) = HashSet seq
let ofList list = (List.toSeq >> ofSeq) list
let ofArray array = (Array.toSeq >> ofSeq) array

let add value set = ``do`` (fun set -> set.Add value) set
let contains value (HashSet set) = set.Contains value
let remove value set = ``do`` (fun set -> set.Remove value) set
let tryGet value set =
    if contains value set
    then Some value
    else None

let toList (HashSet set) = set |> Seq.toList
let toArray (HashSet set) = set |> Seq.toArray
