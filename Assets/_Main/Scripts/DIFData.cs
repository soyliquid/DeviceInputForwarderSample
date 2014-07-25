using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class DIFData {
	public int ID;
	public Vector3 InitialGravity;
	public Vector3 InitialCompassVector;
	public float InitialCompassTrueHeading;
	public Vector3 Acceleration;
	public Vector3 Gravity;
	public Vector3 UserAcceleration;
	public Vector3 RotationRate;
	public Vector3 RotationRateUnbiased;
	public List<Vector3> TouchPoints = new List<Vector3>();
	public List<Vector3> TouchDeltas = new List<Vector3>();
	public List<TouchPhase> TouchPhases = new List<TouchPhase>();
	public List<int> TouchIDs = new List<int>();
	public List<float> TouchTimes = new List<float>();
	public Vector3 CompassVector;
	public float CompassTrueHeading;
	
	public void ClearLists() {
		TouchPoints.Clear();
		TouchDeltas.Clear();
		TouchPhases.Clear();
		TouchIDs.Clear();
		TouchTimes.Clear();
	}
	
	public static byte[] SerializeData(DIFData v) {
		XmlSerializer serializer = new XmlSerializer(typeof(DIFData));
		MemoryStream ms = new MemoryStream();
		serializer.Serialize(ms, v); 
		return ms.ToArray();
	}
	
	public static DIFData DeserializeData(byte[] data) {
		XmlSerializer serializer = new XmlSerializer(typeof(DIFData));
		MemoryStream ms = new MemoryStream(data);
		return (DIFData)serializer.Deserialize(ms);
	}
}