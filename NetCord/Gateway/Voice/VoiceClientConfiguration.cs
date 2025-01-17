﻿using NetCord.Gateway.Voice.UdpSockets;
using NetCord.Gateway.WebSockets;

namespace NetCord.Gateway.Voice;

public class VoiceClientConfiguration
{
    public VoiceApiVersion Version { get; init; } = VoiceApiVersion.V4;
    public IWebSocket? WebSocket { get; init; }
    public IUdpSocket? UdpSocket { get; init; }
    public bool RedirectInputStreams { get; init; }
}
