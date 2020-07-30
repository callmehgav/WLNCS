using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace cesium_dotNet
{
    public class MessageBus
    {
        private ConcurrentDictionary<string, WebSocket> _users = new ConcurrentDictionary<string, WebSocket>();
        public async Task AddUser(WebSocket socket)
        {
            try
            {
                var name = GenerateName();
                var userAddedSuccessfully = _users.TryAdd(name, socket);
                while (!userAddedSuccessfully)
                {
                    name = GenerateName();
                    userAddedSuccessfully = _users.TryAdd(name, socket);
                }
                
                GiveUserTheirName(name, socket).Wait();
                AnnounceNewUser(name).Wait();
                while (socket.State == WebSocketState.Open)
                {
                    var buffer = new byte[1024 * 4];
                    WebSocketReceiveResult socketResponse;
                    var package = new List<byte>();
                    do
                    {
                        socketResponse = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        package.AddRange(new ArraySegment<byte>(buffer, 0, socketResponse.Count));
                    } while (!socketResponse.EndOfMessage);
                    var bufferAsString = System.Text.Encoding.ASCII.GetString(package.ToArray());
                }
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            catch 
            { }
        }

        private string GenerateName()
        {
            var prefix = "Cesium User #";
            Random ran = new Random();
            var name = prefix + ran.Next(1, 1000);
            while (_users.ContainsKey(name))
            {
                name = prefix + ran.Next(1, 1000);
            }
            return name;
        }
        private async Task SendAll(string message)
        {
            await Send(message, _users.Values.ToArray());
        }

        private async Task Send(string message, params WebSocket[] socketsToSendTo)
        {
            var sockets = socketsToSendTo.Where(s => s.State == WebSocketState.Open);
            foreach (var theSocket in sockets)
            {
                var stringAsBytes = System.Text.Encoding.ASCII.GetBytes(message);
                var byteArraySegment = new ArraySegment<byte>(stringAsBytes, 0, stringAsBytes.Length);
                await theSocket.SendAsync(byteArraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task GiveUserTheirName(string name, WebSocket socket)
        {
            var message = new SocketMessage<string>
            {
                MessageType = "name",
                Payload = name
            };
            await Send(message.ToJson(), socket);
        }

        private async Task AnnounceNewUser(string name)
        {
            var message = new SocketMessage<string>
            {
                MessageType = "announce",
                Payload = $"{name} has joined"
            };
            await SendAll(message.ToJson());
        }
    }
}
