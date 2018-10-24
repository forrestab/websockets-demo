using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace Echo.Server
{
    public class Server
    {
        public async void Start(string listenerPrefix)
        {
            HttpListener Listener = new HttpListener();

            Listener.Prefixes.Add(listenerPrefix);
            Listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                HttpListenerContext ListenerContext = await Listener.GetContextAsync();

                if (ListenerContext.Request.IsWebSocketRequest)
                {
                    this.ProcessRequest(ListenerContext);
                }
                else
                {
                    ListenerContext.Response.AsBadRequestAndClose();
                }
            }
        }

        private async void ProcessRequest(HttpListenerContext context)
        {
            WebSocketContext SocketContext = null;

            try
            {
                SocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
            }
            catch (Exception ex)
            {
                context.Response.AsInternalServerErrorAndClose();
                Console.WriteLine("Exception: {0}", ex);

                return;
            }

            WebSocket Socket = SocketContext.WebSocket;

            try
            {
                byte[] ReceiveBuffer = new byte[1024];

                while (Socket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult Result = await Socket.ReceiveAsync(new ArraySegment<byte>(ReceiveBuffer), 
                        CancellationToken.None);

                    if (Result.MessageType == WebSocketMessageType.Close)
                    {
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        Console.WriteLine("Socket closed by client.");
                    }
                    else if (Result.MessageType == WebSocketMessageType.Text)
                    {                        
                        await Socket.SendAsync(new ArraySegment<byte>(ReceiveBuffer, 0, Result.Count), WebSocketMessageType.Text, 
                            Result.EndOfMessage, CancellationToken.None);
                        Console.WriteLine($"Echoed: {Encoding.Default.GetString(ReceiveBuffer).TrimEnd('\0')}");
                    }
                    else
                    {                        
                        await Socket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "Cannot accept binary frame", 
                            CancellationToken.None);
                        Console.WriteLine("Received binary from client.");
                    }

                    Array.Clear(ReceiveBuffer, 0, ReceiveBuffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (Socket != null)
                {
                    Socket.Dispose();
                }
            }
        }
    }
}
