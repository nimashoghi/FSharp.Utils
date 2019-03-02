module FSharp.Utils.Reflection

open System
open System.Collections.Generic
open System.Threading.Tasks
open FSharp.Reflection

let (|OptionType|_|) (``type``: Type) =
    if ``type``.IsGenericType &&
        ``type``.GetGenericTypeDefinition() = typedefof<_ option>
    then Some ``type``.GenericTypeArguments.[0]
    else None

let (|ResultType|_|) (``type``: Type) =
    if ``type``.IsGenericType &&
        ``type``.GetGenericTypeDefinition() = typedefof<Result<_, _>>
    then Some (``type``.GenericTypeArguments.[0], ``type``.GenericTypeArguments.[1])
    else None

let (|NullableType|_|) (``type``: Type) =
    if ``type``.IsGenericType && ``type``.GetGenericTypeDefinition() = typedefof<Nullable<_>>
    then Some``type``.GenericTypeArguments.[0]
    else None

let (|ObservableType|_|) (``type``: Type) =
    ``type``.GetInterfaces()
    |> Array.tryFind (fun ``interface`` ->
        ``interface``.IsGenericType
        && ``interface``.GetGenericTypeDefinition() = typedefof<IObservable<_>>)

let (|TaskType|_|) (``type``: Type) =
    if ``type``.IsGenericType && ``type``.GetGenericTypeDefinition() = typedefof<Task<_>>
    then Some ``type``.GenericTypeArguments.[0]
    else None

let (|ArrayType|_|) (``type``: Type) =
    if ``type``.IsArray
    then Some(``type``.GetElementType())
    else None

let (|ListType|_|) (``type``: Type) =
    if ``type``.IsGenericType && ``type``.GetGenericTypeDefinition() = typedefof<_ list>
    then Some ``type``.GenericTypeArguments.[0]
    else None

let (|SeqType|_|) (``type``: Type) =
    ``type``
        .GetInterfaces()
        |> Array.tryFind(fun ``interface`` ->
            ``interface``.IsGenericType &&
            ``interface``.GetGenericTypeDefinition() = typedefof<IEnumerable<_>>)
        |> Option.map(fun ``interface`` -> ``interface``.GenericTypeArguments.[0])

let (|StringType|_|) ``type`` =
    if ``type`` = typeof<string>
    then Some()
    else None

let (|EnumerableType|_|) (``type``: Type) =
    match ``type`` with
    | StringType -> None
    | ArrayType underlyingType
    | SeqType underlyingType -> Some underlyingType
    | _ -> None

let internal (|CaseTag|_|) i (case: UnionCaseInfo) =
    if case.Tag = i
    then Some()
    else None

// TODO: Memoize this
let internal unionValue f (x: obj) =
    let ``type`` = x.GetType()
    if FSharpType.IsUnion ``type`` then
        let case, fields = FSharpValue.GetUnionFields(x, ``type``)
        f case fields
    else invalidArg "x" "x must be a union type"

#nowarn "25"

let internal optionValue x =
    match box x with
    | null -> None
    | x ->
        unionValue(
            fun case [|value|] ->
                match case with
                | CaseTag 0 -> None
                | CaseTag 1 -> Some value
        ) x

let internal resultValue x =
    unionValue(
        fun case [|value|] ->
            match case with
            | CaseTag 0 -> Ok value
            | CaseTag 1 -> Error value
    ) x

module internal DynamicList =
    let getProperty (``type``: Type) name this =
        typedefof<_ list>
            .MakeGenericType(``type``)
            .GetProperty(name)
            .GetValue(this)

    let isEmpty ``type`` list = getProperty ``type`` "IsEmpty" list |> unbox<bool>
    let head ``type`` list = getProperty ``type`` "Head" list
    let tail ``type`` list = getProperty ``type`` "Tail" list

let listValue (listType: Type) x =
    let isEmpty = DynamicList.isEmpty listType
    let head = DynamicList.head listType
    let tail = DynamicList.tail listType

    let rec run acc (x: obj) =
        if isEmpty x
        then acc
        else run (head x :: acc) (tail x)

    run [] x
    |> List.rev

let (|List|_|) (x: obj) =
    match x.GetType() with
    | ListType ``type`` -> Some(listValue ``type`` x)
    | _ -> None

let (|Option|_|) (x: obj) =
    match x.GetType() with
    | OptionType _ -> Some(optionValue x)
    | _ -> None

let (|Result|_|) (x: obj) =
    match x.GetType() with
    | ResultType _ -> Some(resultValue x)
    | _ -> None

let (|ValidationResult|_|) (x: obj) =
    match x.GetType() with
    | ResultType (_, ListType _) ->
        match resultValue x with
        | Ok value -> Some(Ok value)
        | Error (List errors) -> Some(Error errors)
    | _ -> None
