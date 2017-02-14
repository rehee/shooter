using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using Core;
using Core.Services.Hubs;

namespace Shooter.Web.Hubs
{
    public class SocketHandler
    {
        private const int socketTick = 1000/60;
        public const int BufferSize = 4096;
        WebSocket socket;
        private static List<SocketHandler> webSockets { get; set; } = new List<SocketHandler>();
        SocketHandler(WebSocket socket)
        {
            this.socket = socket;

        }
        public static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;
            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new SocketHandler(socket);
            webSockets.Add(h);
            await h.EchoLoop();
        }
        async Task EchoLoop()
        {

            var hubs = E.env.NewIHubProcessUnit();
            if (!hubs.UnitActive)
            {
                Task postData = new Task((IHubProcessUnit) => {
                    hubs.UnitActive = true;
                    while (hubs.UnitActive)
                    {
                        try
                        {
                            var state = hubs.PushMessage().ToWebSocketOutPut();
                            this.socket.SendAsync(state, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        catch { }
                        System.Threading.Thread.Sleep(socketTick);
                    }
                }, hubs);
                postData.Start();
            }
            while (this.socket.State == WebSocketState.Open)
            {
                var buffer = new byte[BufferSize];
                var seg = new ArraySegment<byte>(buffer);
                var incoming = await this.socket.ReceiveAsync(seg, CancellationToken.None);
                if (incoming.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
                var str = buffer.WebSocketToString(incoming.Count).Replace("\0", "").Split(' ');
                Task processCommand = new Task(() =>
                {
                    hubs.DoCommand(str.ToList());
                    //this.socket.SendAsync(outPut, WebSocketMessageType.Text, true, CancellationToken.None);
                });
                processCommand.Start();
            }
            Console.WriteLine("closed");
            hubs.LeftRoom();
            hubs.UnitActive = false ;
        }
    }
}

namespace System
{
    public static class WebSocketExtend
    {

        public static string WebSocketToString(this byte[] buffer, int count)
        {
            var outgoing = new ArraySegment<byte>(buffer, 0, count);
            var str1 = System.Text.Encoding.UTF8.GetString(outgoing.Array);
            return str1;

        }

        public static ArraySegment<byte> ToWebSocketOutPut(this string input)
        {
            byte[] toBytes = Encoding.UTF8.GetBytes(input);
            var outPut = new ArraySegment<byte>(toBytes, 0, toBytes.Count());
            return outPut;
        }
    }
}
