namespace Hbie

open System.Threading
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging


type Worker(engineConfiguration: EngineConfiguration, logger: ILogger<Worker>) =
    inherit BackgroundService()

    override _.ExecuteAsync(ct: CancellationToken) =
        task {
            logger.LogDebug "Engine starting..."

            Engine.ExecuteEngine(engineConfiguration, ct)

            logger.LogDebug "Engine stopped"
        }
