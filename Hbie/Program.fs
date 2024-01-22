namespace Hbie

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open System.IO
open FSharp.Json


module Program =

    [<EntryPoint>]
    let main args =
        let builder = Host.CreateApplicationBuilder args

        builder.Services.AddHostedService<Worker>(fun sp ->
            // Load engine configuration
            let enginePath = builder.Configuration.GetSection("EnginePath").Value
            let engineJson = File.ReadAllText enginePath
            let engineConfiguration = Json.deserialize<EngineConfiguration> engineJson

            let logger = sp.GetRequiredService<ILogger<Worker>>()

            new Worker(engineConfiguration, logger)) |> ignore

        builder.Build().Run()

        0 // exit code
