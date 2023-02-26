using System.Collections;
using System;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using ArtDotNet.Packets;

namespace ArtDotNet
{
	public class ArtNetClient : MonoBehaviour
	{

		public byte[] DMXdata = new byte[513];		
		public const int PORT = 6454;
		public const string NAME = "ArtNetServer";		
		int universe, net, subnet;

		public string Name { get; set; }

		public IPAddress Address { get; set; }

		public int Port { get; set; }

		UdpCommunicator communicator;

		public event EventHandler<ArtNetPacket> PacketReceived;
		public event EventHandler<ArtPollPacket> PollPacketReceived;
		public event EventHandler<ArtDmxPacket> DmxPacketReceived;



		public ArtNetClient() : this(NAME)
		{

		}

		public ArtNetClient(string name) : this(name, IPAddress.Any, PORT)
		{

		}

		public ArtNetClient(string name, IPAddress address, int port)
		{
			Name = name;
			Address = address;
			Port = port;
		}

	    void Start()
	    {
		    communicator = new UdpCommunicator();
		    communicator.DataReceived += Communicator_DataReceived;
		    communicator.Start(Address, Port);
	    }
	    
		/*void Update(){
			tex.SetPixels32(DMXpix);
			tex.Apply();
			
		}*/
	


		public void Stop()
		{
			communicator.Stop();
		}


		ArtNetPacket packet;
		void Communicator_DataReceived(object sender, UdpPacket e)
		{

			packet = new ArtNetPacket(e.EndPoint, e.RawData);
			if (DMXdata.Length == 0)
            {
				DMXdata = new byte[packet.RawData.Length];
			}
			
			
			
		}
        private void Update()
        {
			if (packet != null && packet.IsValid && packet.RawData.Length == 530)
			{
				Debug.Log("Received From:" + packet.RawData.Length + " DMX " + DMXdata.Length);				
				for (int i = 0; i < 511; i++)
				{
					DMXdata[i] = packet.RawData[i];
				}
				
			}
			
		}
        private void OnDestroy()
        {
			
			communicator.Stop();
			communicator.DataReceived -= Communicator_DataReceived;


		}

    }
}