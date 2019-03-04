namespace FSharp.Utils

open System

[<AutoOpen>]
module TimeSpanExtensions =
    type TimeSpan with
        member this.FromNow = DateTimeOffset.Now + this
