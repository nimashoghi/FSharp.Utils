module FSharp.Utils.UnitTests.Guid

open System
open NUnit.Framework
open Swensen.Unquote
open FSharp.Utils

module ``Guid::Random`` =
    [<Test>]
    let ``should be unique`` () =
        Guid.Random <>! Guid.Random

module ``Guid::toString`` =
    [<Test>]
    let ``basic`` () =
        Guid.Parse "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e"
        |> Guid.toString
        =! "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e"

module ``Guid::get`` =
    [<Test>]
    let ``success`` () =
        Guid.get "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e" =! Guid.Parse "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e"

    [<Test>]
    let ``failure`` () =
        raises <@ Guid.get "jdslfsdjfjdslfks-3b0dfdnlskfsljfd-4da9fdjslkfs-fldskfjsaf7a-e5f50bf3da3e" @>

module ``Guid::tryGet`` =
    [<Test>]
    let ``success`` () =
        Guid.tryGet "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e" =! Some (Guid.Parse "66919b8e-3b0d-4da9-af7a-e5f50bf3da3e")

    [<Test>]
    let ``failure`` () =
        Guid.tryGet "jdslfsdjfjdslfks-3b0dfdnlskfsljfd-4da9fdjslkfs-fldskfjsaf7a-e5f50bf3da3e"
        =! None
