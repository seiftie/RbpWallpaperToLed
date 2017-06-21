using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RbpWallpaperToLed.Core
{
    public static class Sockets
    {
        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int _serverPort = 1807;


        public static void ServerInit(IPAddress ip, int port = _serverPort)
        {
            Console.WriteLine("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(ip, port));
            _serverSocket.Listen(10);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = _serverSocket.EndAccept(AR);
            _clientSockets.Add(socket);
            Console.WriteLine("Client Connected");
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuffer = new byte[received];
            Array.Copy(_buffer, dataBuffer, received);

            // Data uit de buffer halen
            Dictionary<Color, int> colors = Core.Convert.ByteArrayToObject<Dictionary<Color, int>>(dataBuffer);
            string text = Encoding.ASCII.GetString(dataBuffer);

            Console.WriteLine("Request received: " + text);

            string response = string.Empty;

            if (text.ToLower() != "get time")
            {
                response = "Invalid Request";
            }
            else
            {
                response = DateTime.Now.ToLongTimeString();
            }

            byte[] data = Core.Convert.ObjectToByteArray(response);
            //byte[] data = Encoding.ASCII.GetBytes(response);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
    }
}
