module FSharp.Utils.UnitTests.ValueOption

open NUnit.Framework
open FsCheck.NUnit
open Swensen.Unquote
open FSharp.Utils

[<Property>]
let ``ValueOption::ofNullObj`` (value: string) =
    if isNull value
    then ValueOption.ofNullObj value =! ValueNone
    else ValueOption.ofNullObj value =! ValueSome value

[<Property>]
let ``ValueOption::toNullObj`` (value: string) = ValueOption.toNullObj (ValueSome value) =! value
