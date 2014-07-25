using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swing : MonoBehaviour {
	
	public float SensorWeight = 120f;
	public float Smooth = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetObjectPosition();
	}
	
	void SetObjectPosition() {
		DIFData data = DIFReceiver.Instance.ReceivedData;
		if(data == null) {
			return;
		}
		// move
		Vector3 targetPos;
		targetPos = data.UserAcceleration;
//		targetPos = data.RotationRate;
//		targetPos = data.Gravity;
//		targetPos = Vector3.zero;
		transform.localPosition = Vector3.Slerp(transform.localPosition, targetPos, Smooth);		

		// rotate
		Quaternion targetRot;
		Vector3 relativeGravity = (data.Gravity - data.InitialGravity) * SensorWeight;
		float relativeHeadingAngle = data.CompassTrueHeading - data.InitialCompassTrueHeading;
		targetRot = Quaternion.Euler(relativeGravity.y, relativeHeadingAngle / 1.5f, relativeGravity.z);
//		targetRot = Quaternion.Euler(relativeGravity * SensorWeight);
//		targetRot = Quaternion.Euler(data.UserAcceleration * SensorWeight);
//		targetRot = Quaternion.Euler(data.RotationRate * SensorWeight);
//		targetRot = Quaternion.Euler(data.Gravity * SensorWeight);
//		targetRot = Quaternion.identity;
		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Smooth);
	}
}
