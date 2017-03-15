using UnityEngine;

public class CBX : MonoBehaviour {
    public float speed = 10.0f;
    public float rotationSpeed = 10.0F;
	
	void Update () {
        transform.Rotate(Vector3.back, speed * rotationSpeed * Time.deltaTime, Space.World);
    }
}
