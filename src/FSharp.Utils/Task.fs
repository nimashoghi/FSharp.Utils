namespace FSharp.Utils

open System
open System.Threading.Tasks

[<AutoOpen>]
module TaskExtensions =
    open FSharp.Utils.Tasks

    type Task with
        member this.RanToCompletion = this.Status = TaskStatus.RanToCompletion

        static member All (t1: _ Task) =
            uvtask {
                let! t1 = t1
                return t1
            }

        static member All (t1: _ Task, t2: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                return struct (t1, t2)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                return struct (t1, t2, t3)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                return struct (t1, t2, t3, t4)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task, t5: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                return struct (t1, t2, t3, t4, t5)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task, t5: _ Task, t6: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                return struct (t1, t2, t3, t4, t5, t6)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task, t5: _ Task, t6: _ Task, t7: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                return struct (t1, t2, t3, t4, t5, t6, t7)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task, t5: _ Task, t6: _ Task, t7: _ Task, t8: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                let! t8 = t8
                return struct (t1, t2, t3, t4, t5, t6, t7, t8)
            }

        static member All (t1: _ Task, t2: _ Task, t3: _ Task, t4: _ Task, t5: _ Task, t6: _ Task, t7: _ Task, t8: _ Task, t9: _ Task) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                let! t8 = t8
                let! t9 = t9
                return struct (t1, t2, t3, t4, t5, t6, t7, t8, t9)
            }

    type ValueTask with
        static member All (t1: _ ValueTask) =
            uvtask {
                let! t1 = t1
                return t1
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                return struct (t1, t2)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                return struct (t1, t2, t3)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                return struct (t1, t2, t3, t4)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask, t5: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                return struct (t1, t2, t3, t4, t5)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask, t5: _ ValueTask, t6: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                return struct (t1, t2, t3, t4, t5, t6)
            }


        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask, t5: _ ValueTask, t6: _ ValueTask, t7: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                return struct (t1, t2, t3, t4, t5, t6, t7)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask, t5: _ ValueTask, t6: _ ValueTask, t7: _ ValueTask, t8: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                let! t8 = t8
                return struct (t1, t2, t3, t4, t5, t6, t7, t8)
            }

        static member All (t1: _ ValueTask, t2: _ ValueTask, t3: _ ValueTask, t4: _ ValueTask, t5: _ ValueTask, t6: _ ValueTask, t7: _ ValueTask, t8: _ ValueTask, t9: _ ValueTask) =
            uvtask {
                let! t1 = t1
                let! t2 = t2
                let! t3 = t3
                let! t4 = t4
                let! t5 = t5
                let! t6 = t6
                let! t7 = t7
                let! t8 = t8
                let! t9 = t9
                return struct (t1, t2, t3, t4, t5, t6, t7, t8, t9)
            }

[<RequireQualifiedAccess>]
module Task =
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
