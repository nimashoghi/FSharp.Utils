open System.Reactive.Linq
open System.Threading.Tasks
open FSharp.Utils.Tasks

exception MyException

let lockObj = obj ()
let mutable underlying = 1

let taskThatDoesntThrow () =
    vtask {
        printfn "got here %i" underlying
        if false
        then return raise MyException
        else
            lock lockObj (fun () -> underlying <- underlying + 1)
            return underlying
    }

let taskThatThrows () =
    vtask {
        if true
        then return raise MyException
        else return 1
    }

let observable = Observable.Range (start = 0, count = 10)

let observableTask =
    vtask {
        try
            for i in observable do
                let! value = taskThatDoesntThrow ()
                printfn "value is %A" (i + value)
            let! value = taskThatThrows ()
            printfn "last value is %i" value
        with e -> printfn "caught err: %A" e
    }

let mainAsync () =
    vtask {
        try
            let! value = taskThatDoesntThrow ()
            printfn "%A" value
            let! value = taskThatThrows ()
            printfn "%A" value
        with exn -> printfn "Caught exn %A" exn
    }

[<EntryPoint>]
let main argv =
    mainAsync().AsTask().Wait()
    0
