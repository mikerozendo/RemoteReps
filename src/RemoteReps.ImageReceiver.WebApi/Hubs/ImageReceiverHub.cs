using Microsoft.AspNetCore.SignalR;

namespace RemoteReps.ImageReceiver.WebApi.Hubs;

/// <summary>
/// Image Receiver Hub to be used to perform operations with the received buffers
/// </summary>
public sealed class ImageReceiverHub : Hub
{
    public async Task OnReceivedBufferAsync(byte[] buffer)
    {
        var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "received-images");
        Directory.CreateDirectory(outputDirectory);
        var fileName = Path.Combine(outputDirectory, $"{Guid.NewGuid()}.jpg");
        await File.WriteAllBytesAsync(fileName, buffer);
        await Task.CompletedTask;
    }
}