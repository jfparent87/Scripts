using System.Collections;
using UnityEngine;

public class ActivationZoneFollower : MonoBehaviour {

    public GameObject activationZone;

	void Start () {
		
	}
	
	void Update () {
        if (gameObject.transform.position.x != activationZone.transform.position.x || gameObject.transform.position.z != activationZone.transform.position.z)
        {
            gameObject.transform.position = new Vector3(activationZone.transform.position.x, activationZone.transform.position.y + 0.18f, activationZone.transform.position.z);
        }
	}
}
