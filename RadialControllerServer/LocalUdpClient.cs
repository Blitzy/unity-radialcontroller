using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

public class LocalUdpClient : IDisposable
{
    /// <summary>
    /// The id of this local udp client. This id gets attached to the local udp packets 
    /// that are sent from it for verification from receiving local clients.
    /// </summary>
    public string id;

    /// <summary>
    /// Should this local udp client ignore packets that are sent from it. By default this client will receive its own packets.
    /// </summary>
    public bool ignoreDataFromClient;

    private int port;
    private UdpClient udpClient;
    private IPEndPoint localEP;
    private IPEndPoint remoteEP;

    public event Action<LocalUdpPacket> onDataReceived;

    public LocalUdpClient(string id, int port)
    {
        this.id = id;
        this.port = port;
        localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        remoteEP = new IPEndPoint(IPAddress.Broadcast, port);

        udpClient = new UdpClient();
        udpClient.ExclusiveAddressUse = false;
        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        udpClient.Client.Bind(localEP);

        udpClient.BeginReceive(OnDataReceive, null);
    }

    public void Dispose()
    {
        udpClient.Close();
    }

    public void Send(Dictionary<string, object> data)
    {
        var packet = LocalUdpPacket.New(id, data);
        var json = packet.ToJson();

        if (!string.IsNullOrEmpty(json))
        {
            var bytes = Encoding.ASCII.GetBytes(json);
            udpClient.Send(bytes, bytes.Length, remoteEP);
        }
    }

    private void OnDataReceive(IAsyncResult ar)
    {
        IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, port);
        Byte[] receivedBytes = udpClient.EndReceive(ar, ref receivedIpEndPoint);

        // Convert data to ASCII and print in console
        string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
        LocalUdpPacket packet = LocalUdpPacket.FromJson(receivedText);

        if (packet != null)
        {
            bool notify = !ignoreDataFromClient || (ignoreDataFromClient && packet.senderId != id);
            if (notify)
            {
                if (onDataReceived != null)
                {
                    onDataReceived(packet);
                }
            }
        }

        // Restart listening for udp data packages
        udpClient.BeginReceive(OnDataReceive, null);
    }
}

public class LocalUdpPacket
{
    public string senderId;
    public Dictionary<string, object> data;

    public static LocalUdpPacket New(string senderId, Dictionary<string, object> data)
    {
        var packet = new LocalUdpPacket();
        packet.senderId = senderId;
        packet.data = data;

        return packet;
    }

    public static LocalUdpPacket FromJson(string json)
    {
        try
        {
            var packetDict = MiniJSON.Json.Deserialize(json) as Dictionary<string, object>;
            if (packetDict == null)
            {
                return null;
            }

            var packet = new LocalUdpPacket();
            
            // Parse sender id.
            if (packetDict.ContainsKey("sender_id"))
            {
                string senderId = packetDict["sender_id"] as string;
                if (senderId != null)
                {
                    packet.senderId = senderId;
                } 
                else
                {
                    return null;
                }
            }

            // Parse data dictionary.
            if (packetDict.ContainsKey("data"))
            {
                var data = packetDict["data"] as Dictionary<string, object>;
                if (data != null)
                {
                    packet.data = data;
                }
                else
                {
                    return null;
                }
            }

            return packet;
        }
        catch
        {
            return null;
        }
    }

    public string ToJson()
    {
        var packetDict = new Dictionary<string, object>();
        packetDict["sender_id"] = senderId;
        packetDict["data"] = data;

        var json = MiniJSON.Json.Serialize(packetDict);
        return json;
    }
}