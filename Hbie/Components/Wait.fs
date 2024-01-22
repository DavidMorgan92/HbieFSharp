namespace Hbie.Components

open System.Threading
open Hbie


module Wait =

    type Options = {
        seconds: int
    }

    let Execute(options: Options, context: PipelineContext, cancellationToken: CancellationToken) =
        cancellationToken.WaitHandle.WaitOne(options.seconds * 1000) |> ignore
