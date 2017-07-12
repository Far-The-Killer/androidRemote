using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Remote
{
    class MPCSocketInterface
    {
        TcpClient client;
        StreamWriter streamWriter;
        string ip;
        int port;

        public MPCSocketInterface(string ipAddress, int port)
        {
            ip = ipAddress;
            this.port = port;
        }

        public bool SendCommand(string command)
        {
            try
            {
                string commandStr = "wm_command=" + System.Net.WebUtility.UrlEncode(command);
                client = new TcpClient();
                client.Connect(ip, port);
                if (!client.Connected) throw new Exception("Could not connect to MPC server");
                streamWriter = new StreamWriter(client.GetStream());

                StringBuilder streamStr = new StringBuilder();
                streamStr.Append("POST /command.html HTTP/1.0\r\n");
                streamStr.Append("HOST: 192.168.0.40\r\n");
                streamStr.Append("Content-Type: application/x-www-form-urlencoded\r\n");
                streamStr.Append("Content-length: " + commandStr.Length + "\r\n");
                streamStr.Append("\r\n");
                streamStr.Append(commandStr + "\r\n");
                streamStr.Append("\r\n");

                streamWriter.Write(streamStr.ToString());
                streamWriter.Flush();

                Console.Write(streamStr.ToString());

                streamWriter.Close();
                client.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (client.Client != null) client.Close();
                return false;
            }
        }
    }
}