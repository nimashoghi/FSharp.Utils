module FSharp.Utils.UnitTests.Option

open System.Threading.Tasks
open NUnit.Framework
open Swensen.Unquote
open FSharp.Utils

module ``Option::getOrRaiseWith`` =
    [<Test>]
    let ``positive`` () =
        Some 1 |> Option.getOrRaiseWith (fun () -> exn "Hello world") =! 1

    [<Test>]
    let ``negative`` () =
        raises<exn> <@ None |> Option.getOrRaiseWith (fun () -> exn "Hello world") @>

module ``Option::getOrRaise`` =
    [<Test>]
    let ``positive`` () =
        Some 1 |> Option.getOrRaise (exn "Hello world") =! 1

    [<Test>]
    let ``negative`` () =
        raises<exn> <@ None |> Option.getOrRaise (exn "Hello world") @>

module ``Option::ofNullObj`` =
    [<Test>]
    let ``basic test`` () =
        Option.ofNullObj 1
        =! Some 1

module ``Option::toNullObj`` =
    [<Test>]
    let ``basic test`` () =
        Option.toNullObj (Some 1)
        =! 1

module ``Option::bindTask`` =
    [<Test>]
    let ``basic test`` () =
        let result = Option.bindTask (fun value -> Task.FromResult (Some (value + 1))) (Some 2)
        result.Result =! Some 3
