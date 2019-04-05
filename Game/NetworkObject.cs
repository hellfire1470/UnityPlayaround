using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Network;

public class NetworkObject : MonoBehaviour
{


    [SerializeField] private string _host;
    [SerializeField] private int _port;
    UdpClient _networkClient;

    // Use this for initialization
    void Start()
    {
        _networkClient = Manager<UdpClient>.Get("Client");
        _networkClient.DataReceived += OnNetworkReceived;
    }

    private void OnNetworkReceived(object sender, NetworkReceiveEventArgs e)
    {
        Debug.Log("Message received: " + e.Data);
    }

    public void ConnectToServer()
    {
        //_networkClient.Connect ();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
