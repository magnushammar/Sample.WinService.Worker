
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Hosting.WindowsServices
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging.EventLog
open Microsoft.Extensions.Logging
open Sample.Worker
open System
open Microsoft.Extensions.Configuration

[<EntryPoint>]
let main argv =

    let configureServices (services:IServiceCollection) =
        services.AddHostedService<Worker>()
                .Configure (fun (settings:EventLogSettings) ->
                    settings.SourceName <- "Sample Worker Service") |> ignore
        
    async {
        Host.CreateDefaultBuilder(argv)
            .UseWindowsService()
            .ConfigureServices configureServices
            |> fun host -> host.Build().Run() 
    } |> Async.RunSynchronously

    0


        
        
