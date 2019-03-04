module FSharp.Utils.DependencyInjection

open System
open FSharp.Reflection
open Microsoft.Extensions.DependencyInjection

type Injection<'dependencies, 'service> = 'dependencies -> 'service

let internal (|SingleType|TupleType|) ``type`` =
    if FSharpType.IsTuple ``type``
    then TupleType (FSharpType.GetTupleElements ``type``)
    else SingleType ``type``

let resolve<'dependencies> (provider: IServiceProvider) =
    let ``type`` = typeof<'dependencies>
    match ``type`` with
    | SingleType ``type`` when ``type`` = typeof<unit> ->
        box ()
        |> unbox<'dependencies>
    | SingleType ``type`` ->
        provider.GetService ``type``
        |> unbox<'dependencies>
    | TupleType types ->
        let values =
            types
            |> Array.map provider.GetService
        FSharpValue.MakeTuple (values, ``type``)
        |> unbox<'dependencies>

let injectScoped (service: Injection<'dependencies, 'service>) (services: IServiceCollection) =
    services.AddScoped<'service> (
        fun provider ->
            resolve<'dependencies> provider
            |> service
    )

let injectSingleton (service: Injection<'dependencies, 'service>) (services: IServiceCollection) =
    services.AddSingleton<'service> (
        fun provider ->
            resolve<'dependencies> provider
            |> service
    )

let injectTransient (service: Injection<'dependencies, 'service>) (services: IServiceCollection) =
    services.AddTransient<'service> (
        fun provider ->
            resolve<'dependencies> provider
            |> service
    )

type IServiceCollection with
    member this.InjectScoped service = injectScoped service this
    member this.InjectSingleton service = injectSingleton service this
    member this.InjectTransient service = injectTransient service this
