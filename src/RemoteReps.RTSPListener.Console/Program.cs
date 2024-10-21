using LibVLCSharp.Shared;
using Microsoft.Extensions.Configuration;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("configuration.json", optional: false)
    .Build();

var rtspSourceUrl = configurationBuilder.GetSection("RtspSourceUrl").Value;
ArgumentNullException.ThrowIfNull(rtspSourceUrl, nameof(rtspSourceUrl));
await StartRtspClient(rtspSourceUrl);


async Task StartRtspClient(string rtspSourceUrl)
{
    Core.Initialize();
    
    using var libVlc = new LibVLC();
    using var mediaPlayer = new MediaPlayer(libVlc);
    using var media = new Media(libVlc, rtspSourceUrl, FromType.FromLocation);

    mediaPlayer.Media = media;
    mediaPlayer.EncounteredError += (sender, e) =>
    {
        Console.WriteLine("An error has occurred while trying to download the RTSP stream.");
    };

    mediaPlayer.Playing += (sender, e) => { Console.WriteLine("Starting to download the RTSP stream."); };
    mediaPlayer.Stopped += (sender, e) => { Console.WriteLine("Stream download has stopped."); };

    mediaPlayer.Play();

    Console.WriteLine("Press enter to stop");
    Console.ReadLine();

    mediaPlayer.Stop();
    await Task.CompletedTask;
}

