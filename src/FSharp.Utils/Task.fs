namespace FSharp.Utils

open System
open System.Threading.Tasks

[<AutoOpen>]
module TaskExtensions =
    type Task with
        member this.RanToCompletion = this.Status = TaskStatus.RanToCompletion

[<RequireQualifiedAccess>]
module Task =
    let completed = Task.FromResult  ()

    let continueWith
        (f: 'input Task -> unit)
        (x: 'input Task)
        : unit =
        x.ContinueWith(Action<_> f)
        |> ignore

    let map
        (f: 'input -> 'output)
        (x: 'input Task)
        : 'output Task =
        let source = TaskCompletionSource ()
        x
        |> continueWith (
            fun task ->
                if task.RanToCompletion then source.SetResult (f task.Result)
                else if task.IsCanceled then source.SetCanceled ()
                else if task.IsFaulted then source.SetException task.Exception
        )
        source.Task

    let bind
        (f: 'input -> 'output Task)
        (x: 'input Task)
        : 'output Task =
        let source = TaskCompletionSource ()
        x
        |> continueWith (
            fun task ->
                if task.IsCompleted then
                    f task.Result
                    |> continueWith (
                        fun task ->
                            if task.IsCompleted then source.SetResult task.Result
                            else if task.IsCanceled then source.SetCanceled ()
                            else if task.IsFaulted then source.SetException task.Exception
                    )
                else if task.IsCanceled then source.SetCanceled ()
                else if task.IsFaulted then source.SetException task.Exception
        )
        source.Task
