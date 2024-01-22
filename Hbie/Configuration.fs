namespace Hbie

open FSharp.Json


type ComponentDefinition =
    | [<JsonUnionCase("wait")>] WaitDefinition of Components.Wait.Options
    | [<JsonUnionCase("print")>] PrintDefinition of Components.Print.Options


type ComponentConfiguration = {
    name: string
    definition: ComponentDefinition
}


type PipelineType =
    | Tcp


type PipelineConfiguration = {
    name: string
    ``type``: PipelineType option
    components: ComponentConfiguration array
}


type EngineConfiguration = {
    name: string
    pipelines: PipelineConfiguration array
}
