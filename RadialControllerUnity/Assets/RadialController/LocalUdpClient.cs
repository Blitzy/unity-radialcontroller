using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class LocalUdpClient
{
    private int port;
    private UdpClient udpClient;

    public LocalUdpClient(int port)
    {
        this.port = port;
    }

    public void Connect()
    {
        if (udpClient == null)
        {
            udpClient = new UdpClient();
            udpClient.Connect("127.0.0.1", port);
            udpClient.BeginReceive(OnDataReceive, null);
        }
    }

    public void SendMessage(string msg)
    {
        //if (udpClient != null)
        //{
        //    var bytes = Encoding.ASCII.GetBytes(msg);
        //    udpClient.Send(bytes, bytes.Length);
        //}
        var sendUdpClient = new UdpClient(port);
        var bytes = Encoding.ASCII.GetBytes(msg);
        sendUdpClient.Send(bytes, bytes.Length, "127.0.0.1", port);

    }

    private void OnDataReceive(IAsyncResult ar)
    {
        UnityEngine.Debug.Log("no?");
        IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, port);
        Byte[] receivedBytes = udpClient.EndReceive(ar, ref receivedIpEndPoint);

        // Convert data to ASCII and print in console
        string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
        UnityEngine.Debug.Log(receivedIpEndPoint + ": " + receivedText);

        // Restart listening for udp data packages
        udpClient.BeginReceive(OnDataReceive, null);
    }
}