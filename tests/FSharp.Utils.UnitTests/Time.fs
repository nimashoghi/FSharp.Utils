module FSharp.Utils.UnitTests.Time

open System
open NUnit.Framework
open Swensen.Unquote
open FSharp.Utils

let (=!!) (lhs: DateTimeOffset) (rhs: DateTimeOffset) =
    (lhs - rhs).TotalMilliseconds <=! 5.

module ``TimeSpan::FromNow`` =
    [<Test>]
    let ``basic test`` () =
        TimeSpan.FromDays(1.).FromNow =!! (DateTimeOffset.Now + TimeSpan.FromDays 1.)
