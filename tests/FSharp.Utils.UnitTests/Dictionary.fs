module FSharp.Utils.UnitTests.Dictionary

open System.Collections.Generic
open NUnit.Framework
open Swensen.Unquote

open FSharp.Utils

let (=!!) (actual: #IDictionary<'key, 'value>) (expected: ('key * 'value) list) =
    Dictionary.toList actual =! expected

module ``empty`` =
    [<Test>]
    let ``should actually be empty`` () =
        Dictionary.empty =!! []

module ``contains`` =
    [<Test>]
    let ``positive`` () =
        let dict = Dictionary ()
        dict.[1] <- 2
        dict |> Dictionary.contains 1 =! true

    [<Test>]
    let ``negative`` () =
        Dictionary.empty |> Dictionary.contains 1 =! false

module ``add`` =
    [<Test>]
    let ``should add our value properly if doesn't exist`` () =
        Dictionary.empty
        |> Dictionary.add 1 2
        =!! [1, 2]

    [<Test>]
    let ``should update the value if exists`` () =
        let myDict = Dictionary ()
        myDict.[1] <- 2
        myDict
        |> Dictionary.add 1 3
        =!! [1, 3]

    [<Test>]
    let ``should update the value if exists 2`` () =
        let myDict = Dictionary ()
        myDict.[1] <- 2
        myDict
        |> Dictionary.add 1 2
        =!! [1, 2]

module ``remove`` =
    [<Test>]
    let ``exists`` () =
        let dict = Dictionary ()
        dict.[1] <- 2
        dict
        |> Dictionary.remove 1
        =!! []

    [<Test>]
    let ``does not exist`` () =
        raises<KeyNotFoundException> <@ Dictionary () |> Dictionary.remove 1 @>

module ``tryRemove`` =
    [<Test>]
    let ``exists`` () =
        let dict = Dictionary ()
        dict.[1] <- 2
        dict
        |> Dictionary.tryRemove 1
        =!! []

    [<Test>]
    let ``does not exist`` () =
        Dictionary ()
        |> Dictionary.tryRemove 1
        =!! []

module ``ofList`` =
    [<Test>]
    let ``should create empty dict for empty list`` () =
        Dictionary.ofList [] =!! []

    [<Test>]
    let ``should create dict with values for nonempty list`` () =
        Dictionary.ofList [1, 2] =!! [1, 2]

    [<Test>]
    let ``should override existing values for list with repeating keys`` () =
        Dictionary.ofList [1, 2; 1, 3] =!! [1, 3]

module ``toList`` =
    [<Test>]
    let ``should return empty list for empty dict`` () =
        Dictionary.empty
        |> Dictionary.toList
        =! []

    [<Test>]
    let ``should return nonempty list for nonempty dict`` () =
        let dict = Dictionary ()
        dict.[1] <- 2
        dict.[3] <- 2
        dict
        |> Dictionary.toList
        =! [1, 2; 3, 2]

module ``tryFind`` =
    [<Test>]
    let ``value exists`` () =
        let dict = Dictionary ()
        dict.[1] <- 2
        dict
        |> Dictionary.tryFind 1
        =! Some 2

    [<Test>]
    let ``value does not exist`` () =
        Dictionary.empty
        |> Dictionary.tryFind 1
        =! None

module ``merge`` =
    [<Test>]
    let ``empty`` () =
        (Dictionary.empty, Dictionary.empty)
        ||> Dictionary.merge
        =!! []

    [<Test>]
    let ``empty lhs`` () =
        (Dictionary.empty, Dictionary.ofList [1, 2])
        ||> Dictionary.merge
        =!! [1, 2]

    [<Test>]
    let ``empty rhs`` () =
        (Dictionary.ofList [1, 2], Dictionary.empty)
        ||> Dictionary.merge
        =!! [1, 2]

    [<Test>]
    let ``no empty no overlap`` () =
        (Dictionary.ofList [1, 2], Dictionary.ofList [2, 2])
        ||> Dictionary.merge
        =!! [1, 2; 2, 2]

    [<Test>]
    let ``no empty with overlap`` () =
        (Dictionary.ofList [1, 2], Dictionary.ofList [1, 3])
        ||> Dictionary.merge
        =!! [1, 3]

module ``count`` =
    [<Test>]
    let ``empty`` () =
        Dictionary.empty
        |> Dictionary.count
        =! 0

    [<Test>]
    let ``non empty`` () =
        Dictionary.ofList [1, 2; 3, 4]
        |> Dictionary.count
        =! 2

module ``exists`` =
    [<Test>]
    let ``empty`` () =
        Dictionary ()
        |> Dictionary.exists (fun _ _ -> true)
        =! false

    [<Test>]
    let ``non empty false`` () =
        Dictionary.ofList [1, 2]
        |> Dictionary.exists (fun key value -> key = 1 && value = 3)
        =! false

    [<Test>]
    let ``non empty true`` () =
        Dictionary.ofList [1, 2]
        |> Dictionary.exists (fun key value -> key = 1 && value = 2)
        =! true

module ``ofObj`` =
    [<Test>]
    let ``empty object`` () =
        Dictionary.ofObj {||}
        |> Option.get
        =!! []

    [<Test>]
    let ``basic object`` () =
        Dictionary.ofObj {|Name = "Nima"|}
        |> Option.get
        =!! ["Name", box "Nima"]

    [<Test>]
    let ``nested object`` () =
        let dict =
            Dictionary.ofObj {|Name = "Nima"; Home = {|Country = "USA"|}|}
            |> Option.get

        dict.["Name"] =! box "Nima"
        unbox<Dictionary<string, obj>> dict.["Home"] =!! ["Country", box "USA"]
