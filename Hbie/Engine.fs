namespace Hbie

open System.Threading


module Engine =

    let ExecuteComponent(componentDefinition: ComponentDefinition, context: PipelineContext, cancellationToken: CancellationToken) =
        match componentDefinition with
        | WaitDefinition options -> Components.Wait.Execute(options, context, cancellationToken)
        | PrintDefinition options -> Components.Print.Execute(options, context, cancellationToken)

    let ExecuteComponents(pipelineConfiguration: PipelineConfiguration, context: PipelineContext, cancellationToken: CancellationToken) =
        for ``component`` in pipelineConfiguration.components do
            ExecuteComponent(``component``.definition, context, cancellationToken)

    let ExecuteTcpPipeline(pipelineConfiguration: PipelineConfiguration, cancellationToken: CancellationToken) =
        // TODO: Listen loop

        let context = { message = "" }

        ExecuteComponents(pipelineConfiguration, context, cancellationToken)

    let ExecuteNormalPipeline(pipelineConfiguration: PipelineConfiguration, cancellationToken: CancellationToken) =
        let context = { message = "" }

        ExecuteComponents(pipelineConfiguration, context, cancellationToken)

    let ExecutePipeline(pipelineConfiguration: PipelineConfiguration, cancellationToken: CancellationToken) =
        async {
            while not cancellationToken.IsCancellationRequested do
                match pipelineConfiguration.``type`` with
                | None -> ExecuteNormalPipeline(pipelineConfiguration, cancellationToken)
                | Some(Tcp) -> ExecuteTcpPipeline(pipelineConfiguration, cancellationToken)
        }

    let ExecuteEngine(engineConfiguration: EngineConfiguration, cancellationToken: CancellationToken) =
        engineConfiguration.pipelines
            |> Seq.map (fun config -> ExecutePipeline(config, cancellationToken))
            |> Async.Parallel
            |> Async.RunSynchronously
            |> ignore
