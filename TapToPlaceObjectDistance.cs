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
        /*
        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
        }*/
    }

        

        // Update is called once per frame
    void Update()
    {
        if (placing)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.0f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.020f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            //this.transform.position.Set(0,0,2);
            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.rotation = toQuat;
        }
    }
}

