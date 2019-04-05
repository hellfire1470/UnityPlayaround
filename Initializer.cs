using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;

public class Initializer {

	private static Thread _networkReconnection;
	private static UdpClient _networkClient;

	[RuntimeInitializeOnLoadMethod]
	public static void Initialize(){
		_networkClient = new UdpClient ("127.0.0.1", 9052);
		_networkClient.Connect ();
		Manager<UdpClient>.Add ("Client", _networkClient);

		//_networkClient.ConnectionLost += OnConnectionLost;
		//_networkClient.Connected += OnConnected;

		//OnConnectionLost (null, null);
	}

	protected static void OnConnectionLost(object sender, System.EventArgs e){
		//_networkReconnection = new Thread (Connect);
		//_networkReconnection.Start ();
	}

	protected static void OnConnected(object sender, System.EventArgs e){
		//_networkReconnection.Abort();
		//_networkReconnection = null;
	}

	private static void Connect(){
//		while(!_networkClient.Connect()){
//			Debug.Log("Connecting to Server ...");
//			Thread.Sleep(1000);
//		}
	}

}
