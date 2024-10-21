using Microsoft.Extensions.Configuration;

namespace RemoteReps.RTSPListener.Console;

public static class ConfigurationBuilderFactory
{
    private static IConfigurationRoot GetConfigurationRoot()
    {
        var configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("configuration.json", optional: false)
            .Build();

        return configurationRoot;
    }

    public static (string sourceStreamUrl, string webSocketUri) GetConfiguration()
    {
        var configurationRoot = GetConfigurationRoot();
        var rtspSourceUrl = configurationRoot.GetSection("RtspSourceUrl").Value;
        var webSocketUri = configurationRoot.GetSection("WebSocketUri").Value;
        ArgumentNullException.ThrowIfNull(rtspSourceUrl, nameof(rtspSourceUrl));
        ArgumentNullException.ThrowIfNull(webSocketUri, nameof(webSocketUri));
        return (rtspSourceUrl, webSocketUri);
    }
}