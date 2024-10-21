# Remote Reps Buffer Hub

## Prerequisites

#### .NET 8
**.NET 8**: Make sure to install the latest stable version of .NET 8. You can download it from the official [.NET website](https://dotnet.microsoft.com/download/dotnet/8.0).

#### FFmpeg Intallation
1. **FFmpeg**: Download FFmpeg from the following link: [FFmpeg Builds](https://www.gyan.dev/ffmpeg/builds/). Follow the instructions provided on the site to install FFmpeg on your machine.

2. ![./assets/img_5.png](.\assets\img_5.png)

**Set FFmpeg Path**: After installing FFmpeg, ensure to define the path to the FFmpeg executable in your system's environment variables so that it can be accessed globally.
For example: once you get the zip folder, you need just to access the FFmpeg's bin folder and set its path to the system environment variables.

**Full path Example**
"C:\Users\micha\Downloads\ffmpeg-2024-10-21-git-baa23e40c1-full_build\ffmpeg-2024-10-21-git-baa23e40c1-full_build\bin\ffmpeg"

![./assets/img_7.png](.\assets\img_7.png)

![/assets/img_8.png](.\assets\img_8.png)

## About the project
There are two projects in the solution
1. RemoteReps.ImageReceiver.WebApi
2. RemoteReps.RTPSListener.Console

![./assets/img_6.png](.\assets\img_6.png)

The first one is a basic Asp.Net Core web Api project that has a Hub configured on path: /hubs/image-receiver
This hub contains a Method to be invoked in order to get the frame saved into the folder "received-images"

The second one is just a C# Console Application, it was crated to just receive frames from a RTSP connection, send the using websockets with SignalR.Client package and then, just wait a little to continue receiving the frames.

The RTSP source is uri is configured under the configuration.json that inside RemoteReps.RTPSListener.Console's root file system, that has Websocket source path configured as well.

```json
{
    "RtspSourceUrl": "http://pendelcam.kip.uni-heidelberg.de/mjpg/video.mjpg",
    "WebSocketUri": "ws://localhost:5260/hubs/image-receiver"
}
```

All generated frames are going to be stored under the folder "received-images" that is based under the path: .\RemoteReps\src\RemoteReps.ImageReceiver.WebApi\received-images

![./assets/img_4.png](.\assets\img_4.png)

## How to clone

After completing the prerequisites, clone this repository to your local machine.

   ```bash
   https://github.com/mikerozendo/RemoteReps.git
   cd RemoteReps
   ```

## How to run

1. Once the project is already on your local machine, you just need to set Multiple startup projects 
2. WebApi is the first one in the priority order because ConsoleApp is completely Dependent on WebApi websocket
3. Start both projects at the same time and just keep it running for a while, you will notice that .\RemoteReps.ImageReceiver.WebApi\received-images will be populated continuously
3. ![./assets/img_2.png](.\assets\img_2.png) 


## Expected output
Once both applications are kept alive, the received-images is going to be populated continuously
![./assets/img_3.png](.\assets\img_3.png)



