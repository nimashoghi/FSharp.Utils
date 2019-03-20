[<RequireQualifiedAccess>]
module FSharp.Utils.List

let uniqueBy f list =
    list
    |> List.groupBy f
    |> List.map (fun (_, group) -> List.head group)
