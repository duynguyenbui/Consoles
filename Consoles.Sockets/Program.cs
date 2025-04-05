using System.Net;
using System.Net.Sockets;
using System.Text;
using Consoles.SocketCommon;

namespace Consoles.SocketServer
{
    internal class Program
    {
        private static readonly Dictionary<Socket, string> Clients = new();

        static async Task Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, Constants.DEFAULT_PORT);
            var serverSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(endpoint);
            serverSocket.Listen();
            Console.WriteLine($"[Server] Chat room is live on port {Constants.DEFAULT_PORT}");

            while (true)
            {
                var clientSocket = await serverSocket.AcceptAsync();
                _ = Task.Run(() => HandleClientAsync(clientSocket));
            }
        }

        private static async Task HandleClientAsync(Socket socket)
        {
            var buffer = new byte[1024];
            string username = null;

            try
            {
                await SendAsync(socket, Constants.WELCOME_MESSAGE);

                while (true)
                {
                    var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) return;

                    var nameAttempt = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                    lock (Clients)
                    {
                        if (!Clients.Values.Contains(nameAttempt))
                        {
                            Clients[socket] = nameAttempt;
                            username = nameAttempt;
                            break;
                        }
                    }

                    await SendAsync(socket, Constants.USERNAME_TAKEN);
                }

                await BroadcastAsync($"👋 {username} joined the chat.", socket);
                Console.WriteLine($"[Server] {username} joined from {socket.RemoteEndPoint}");

                while (true)
                {
                    var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) break;

                    var msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                    if (msg.Equals("/exit", StringComparison.OrdinalIgnoreCase))
                    {
                        await SendAsync(socket, Constants.GOODBYE_MESSAGE);
                        break;
                    }

                    Console.WriteLine($"[{username}]: {msg}");
                    await BroadcastAsync($"[{username}]: {msg}", socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Server] Error with {username ?? "unknown"}: {ex.Message}");
            }
            finally
            {
                if (username != null)
                {
                    Console.WriteLine($"[Server] {username} left.");
                    lock (Clients)
                    {
                        Clients.Remove(socket);
                    }

                    await BroadcastAsync($"👋 {username} has left the chat.");
                }

                socket.Close();
                socket.Dispose();
            }
        }

        private static async Task BroadcastAsync(string message, Socket exclude = null)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            lock (Clients)
            {
                foreach (var client in Clients.Keys.ToList())
                {
                    if (client != exclude)
                    {
                        try { client.SendAsync(bytes, SocketFlags.None); }
                        catch { Clients.Remove(client); }
                    }
                }
            }
        }

        private static async Task SendAsync(Socket socket, string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(bytes, SocketFlags.None);
        }
    }
}
