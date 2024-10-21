using Microsoft.AspNetCore.SignalR.Client;

namespace RemoteReps.RTSPListener.Web.Repositories;

public sealed class SignalRHubRepository
{
    private static HubConnection? _connection;

    public SignalRHubRepository(string hubPath)
    {
        ConfigureHubConnection(hubPath);
    }

    private static void ConfigureHubConnection(string hubPath)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(hubPath)
            .Build();

        // _connection.On<string>("ReceiveMessage", (message) =>
        // {
        // });

        _connection.StartAsync().ConfigureAwait(true).GetAwaiter().GetResult();
    }
    
    public async Task SendBufferAsync(byte[] buffer)
    {
        ArgumentNullException.ThrowIfNull(_connection);
        await _connection.InvokeAsync("OnReceivedBufferAsync", buffer);
    }
}