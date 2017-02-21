using UnityEngine;

public class TapToPlaceObjectDistance : MonoBehaviour
{
    public bool placing = false;
    public Transform target;
    public float speed;
    public GameObject mainCamera;
    private Vector3 targetPosition;

    // Called by GazeGestureManager when the user performs a Select gesture
    private void Start()
    {
        target = mainCamera.transform;
        targetPosition = target.position;
    }

    void OnSelect()
    {
        placing = !placing;
    }

    void Update()
    {
        if (placing)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.0f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.rotation = toQuat;
        }
    }
}

