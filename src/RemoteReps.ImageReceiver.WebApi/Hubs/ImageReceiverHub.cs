using Microsoft.AspNetCore.SignalR;

namespace RemoteReps.ImageReceiver.WebApi.Hubs;

/// <summary>
/// Image Receiver Hub to be used to perform operations with the received buffers
/// </summary>
public sealed class ImageReceiverHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "ImageStores");
    }
}