namespace FSharp.Utils

open System

[<AutoOpen>]
module GuidExtensions =
    type Guid with
        static member Random = Guid.NewGuid ()

[<RequireQualifiedAccess>]
module Guid =
    let toString (guid: Guid) = guid.ToString ()
    let get (input: string) = Guid.Parse input
    let tryGet (input: string) =
        match Guid.TryParse input with
        | true, guid -> Some guid
        | _ -> None
