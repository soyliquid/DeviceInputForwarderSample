using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmController : MonoBehaviour {

	public int ControlID = 0;
	public float SensorWeight = 60f;
	public float Smooth = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DoTransform();
	}
	
	void DoTransform() {
		// store data.
		DIFData data = DIFReceiver.Instance.ReceivedData;

		// return when no data.
		if(data == null) return;
		// return when other id.
		if(data.ID != ControlID) return;

		// do move.
		Vector3 targetPos;
		targetPos = data.UserAcceleration;
//		targetPos = data.RotationRate;
//		targetPos = data.Gravity;
//		targetPos = Vector3.zero;
//		transform.localPosition = Vector3.Slerp(transform.localPosition, targetPos, Smooth);		

		// do rotate.
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
