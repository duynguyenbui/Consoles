using System.Net;
using System.Net.Sockets;
using System.Text;
using Consoles.SocketCommon;

namespace Consoles.SocketClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, Constants.DEFAULT_PORT);
            var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Console.WriteLine("[Client] Connecting to chat room...");
                await socket.ConnectAsync(endpoint);

                var buffer = new byte[1024];

                // Handle welcome & username input
                while (true)
                {
                    var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                    var message = Encoding.UTF8.GetString(buffer, 0, received);
                    Console.WriteLine($"[Server] {message}");

                    Console.Write(">>> ");
                    var name = Console.ReadLine();
                    var nameBytes = Encoding.UTF8.GetBytes(name);
                    await socket.SendAsync(nameBytes, SocketFlags.None);

                    if (!message.Contains("already taken", StringComparison.OrdinalIgnoreCase)) break;
                }

                // Start receiving messages
                _ = Task.Run(() => ReceiveLoop(socket));

                // Send messages
                while (true)
                {
                    Console.Write("You: ");
                    var msg = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(msg)) continue;

                    var bytes = Encoding.UTF8.GetBytes(msg);
                    await socket.SendAsync(bytes, SocketFlags.None);

                    if (msg.Equals("/exit", StringComparison.OrdinalIgnoreCase)) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client] Error: {ex.Message}");
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket.Dispose();
                Console.WriteLine("[Client] Disconnected from chat room.");
            }
        }

        private static async Task ReceiveLoop(Socket socket)
        {
            var buffer = new byte[1024];

            try
            {
                while (true)
                {
                    var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) break;

                    var msg = Encoding.UTF8.GetString(buffer, 0, received);
                    Console.WriteLine($"\n{msg}\nYou: ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Client] Lost connection: {ex.Message}");
            }
        }
    }
}
