using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class DIFReceiver : MonoBehaviour {

	// singleton.
	public static DIFReceiver Instance {
		get { return _instance; }
	}
	private static DIFReceiver _instance;

	private UdpClient udp;
	private IPEndPoint currentEndPoint;
	private bool isActive;
	
	public DIFData ReceivedData;
	public int ListenPort = 3960;

	// prepare singleton.
	void Awake() {
		_instance = this;
	}
	
	// Use this for initialization
	void Start () {
		LinkStart();
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive) {
			ReceiveSensorValue();
		}
	}
	
	void ReceiveSensorValue() {
		// receive data when it came
		if(udp != null && udp.Available > 0) {				
			StartCoroutine(AsyncReadValue());
		}	
	}
	
	IEnumerator AsyncReadValue() {
		IPEndPoint receiveEp = new IPEndPoint(IPAddress.Any, 0);
		byte[] buff = udp.Receive(ref receiveEp);
		ReceivedData = DIFData.DeserializeData(buff);
		yield return null;
	}
	
	public void Reset() {
		string ip = PlayerPrefs.GetString("ip", "127.0.0.1");
		Debug.Log("Reset-" + ip + ":" + ListenPort);
		currentEndPoint = new IPEndPoint(IPAddress.Parse(ip), ListenPort);
		if(udp != null) udp.Close();
		udp = new UdpClient(ListenPort);
	}

	public void LinkStart() {
		Reset();
		isActive = true;
	}
	
	public void LinkStop() {
		if(udp != null) udp.Close();
		isActive = false;
	}
}
