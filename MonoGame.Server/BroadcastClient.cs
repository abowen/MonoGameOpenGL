﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MonoGame.Common.Networking;

namespace MonoGame.Server
{
    public class BroadcastClient
    {
        private const int udpRangeStart = 15123;
        private const int localMaximumPortCount = 16;
        private UdpClient udpClient;
        private IPEndPoint udpReceiveEndPoint;
        private List<IPEndPoint> udpSendEndPoints;
        public int LocalPort;
        public bool IsListening = false;
        public Queue<NetworkMessage> MessagesReceived = new Queue<NetworkMessage>();

        public BroadcastClient()
        {
            BeginListening();
            SetupSendPorts();
        }

        private void BeginListening()
        {
            var portTestCount = 0;
            var udpPortFound = false;
            do
            {
                try
                {
                    LocalPort = udpRangeStart + portTestCount;
                    udpReceiveEndPoint = new IPEndPoint(IPAddress.Any, LocalPort);
                    udpClient = new UdpClient(udpReceiveEndPoint);
                    udpPortFound = true;
                }
                catch (SocketException)
                {
                    portTestCount++;
                }
            } while (!udpPortFound && portTestCount < localMaximumPortCount);

            if (udpPortFound)
            {
                udpClient.BeginReceive(UdpMessageReceived, udpClient);
                IsListening = true;
            }
        }

        private void UdpMessageReceived(IAsyncResult asyncResult)
        {
            var receivedBytes = udpClient.EndReceive(asyncResult, ref udpReceiveEndPoint);
            udpClient.BeginReceive(UdpMessageReceived, udpClient);
            if (udpReceiveEndPoint.Port != LocalPort)
            {
                MessagesReceived.Enqueue(new NetworkMessage(udpReceiveEndPoint.Address, udpReceiveEndPoint.Port, receivedBytes));
            }
        }

        private void SetupSendPorts()
        {
            udpSendEndPoints = new List<IPEndPoint>();
            for (var sendPortOffset = 0; sendPortOffset < localMaximumPortCount; sendPortOffset++)
            {
                udpSendEndPoints.Add(new IPEndPoint(IPAddress.Broadcast, udpRangeStart + sendPortOffset));
            }
        }

        public void Send(byte[] data)
        {
            foreach (var endPoint in udpSendEndPoints)
            {
                udpClient.BeginSend(data, data.Length, endPoint, UdpMessageSent, udpClient);
            }
        }

        public void Send(int destinationPort, byte[] data)
        {
            var endPoint = udpSendEndPoints.First(udp => udp.Port == destinationPort);            
            udpClient.BeginSend(data, data.Length, endPoint, UdpMessageSent, udpClient);            
        }

        private void UdpMessageSent(IAsyncResult asyncResult)
        {

        }
    }
}
