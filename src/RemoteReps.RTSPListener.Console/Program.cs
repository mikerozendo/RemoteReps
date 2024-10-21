using Microsoft.AspNetCore.SignalR.Client;
using RemoteReps.RTSPListener.Console;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;


await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
var configuration = ConfigurationBuilderFactory.GetConfiguration();
var websocketUri = new Uri(configuration.webSocketUri);
var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "temp_frames");
Directory.CreateDirectory(outputDirectory);

var connection = new HubConnectionBuilder()
    .WithUrl(websocketUri)
    .Build();

try
{
    await connection.StartAsync();
    while (true)
    {
        await Task.Delay(1000);
        await SendBufferAsync();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    if (Directory.Exists(outputDirectory))
        Directory.Delete(outputDirectory, true);
}

async Task SendBufferAsync()
{
    var ffmpeg = FFmpeg.Conversions.New()
        .AddParameter($"-i {configuration.sourceStreamUrl}")
        .AddParameter("-vf fps=1")
        .SetOutput(Path.Combine(outputDirectory, $"{Guid.NewGuid()}-frame.jpg"));

    var conversion = ffmpeg.Start();
    var files = Directory.GetFiles(outputDirectory, "*.jpg");
    await Task.Delay(200);

    var invokedTasks = files.Select(async x =>
    {
        var fileBytes = await File.ReadAllBytesAsync(x);
        File.Delete(x);
        await connection.InvokeAsync("OnReceivedBufferAsync", fileBytes);
    }).ToList();
}