module FSharp.Utils.UnitTests.List

open FsCheck.NUnit
open Swensen.Unquote

open FSharp.Utils

[<Property>]
let ``uniqueBy`` (list: int list) =
    list
    |> List.uniqueBy id
    =!
    (list
    |> List.groupBy id
    |> List.map (fun (i, _) -> i))
