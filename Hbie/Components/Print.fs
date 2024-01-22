namespace Hbie.Components

open System
open System.Threading
open Hbie


module Print =

    type Options = {
        message: string
    }

    let Execute(options: Options, context: PipelineContext, cancellationToken: CancellationToken) =
        Console.WriteLine(options.message)
