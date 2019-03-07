module FSharp.Utils.UnitTests.HashSet

open NUnit.Framework
open Swensen.Unquote

open FSharp.Utils

let inline (=!!) set list = (HashSet.toList set |> List.sort) =! (list |> List.sort)

module ``ofList`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofList [1; 2; 3] =!! [1; 2; 3]

module ``ofSeq`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofSeq [1; 2; 3] =!! [1; 2; 3]

module ``ofArray`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofArray [|1; 2; 3|] =!! [1; 2; 3]

module ``add`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.add 5 =!! [1; 2; 3; 5]

module ``contains`` =
    [<Test>]
    let ``positive`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.contains 3 =! true

    [<Test>]
    let ``negative`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.contains 5 =! false

module ``tryGet`` =
    [<Test>]
    let ``positive`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.tryGet 3 =! Some 3

    [<Test>]
    let ``negative`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.tryGet 5 =! None

module ``remove`` =
    [<Test>]
    let ``positive`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.remove 3 =!! [1; 2]

    [<Test>]
    let ``negative`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.remove 4 =!! [1; 2; 3]

module ``toList`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.toList =! [1; 2; 3]

module ``toArray`` =
    [<Test>]
    let ``basic test`` () =
        HashSet.ofList [1; 2; 3] |> HashSet.toArray =! [|1; 2; 3|]
