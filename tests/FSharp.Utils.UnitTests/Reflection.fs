module FSharp.Utils.UnitTests.Reflection

open NUnit.Framework
open Swensen.Unquote

open FSharp.Utils.Reflection

module ``Handlers`` =
    module ``optionValue`` =
        [<Test>]
        let ``some`` () =
            Some "hello"
            |> box
            |> optionValue
            =! Some (box "hello")

        [<Test>]
        let ``none`` () =
            None
            |> box
            |> optionValue
            =! None

    module ``resultValue`` =
        [<Test>]
        let ``ok`` () =
            Ok "hello"
            |> box
            |> resultValue
            =! Ok (box "hello")

        [<Test>]
        let ``error`` () =
            Error "some error"
            |> box
            |> resultValue
            =! Error (box "some error")
