using UnityEngine;

public class TapToPlaceActivationZoneThree : MonoBehaviour {

    public float speed;
    public Camera mainCamera;
    public float distanceToCameraWhenPlacing = 1.2f;

    private bool placing;
    private RoomManager roomManager;
    private Vector3 targetPosition;
    private ActivationZoneAnchor anchor;
    private float step;
    private Quaternion activationZoneRotation;
    private float heightCorrection = 1.5f;

    void Start()
    {
        placing = false;
        targetPosition = mainCamera.transform.position;
        anchor = GetComponent<ActivationZoneAnchor>();
        roomManager = GetComponentInParent<RoomManager>();
    }

    void OnSelect()
    {
        if (roomManager.editionMode)
        {
            placing = !placing;
            if (placing)
            {
                anchor.freeAnchor();
            }

            if (!placing)
            {
                anchor.lockAnchor();
            }
        }
    }

    void Update()
    {
        if (placing && roomManager.editionMode)
        {
            placeActivationZoneInFrontOfCamera();
        }
    }

    private void placeActivationZoneInFrontOfCamera()
    {
        targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, mainCamera.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        activationZoneRotation = mainCamera.transform.localRotation;
        activationZoneRotation.x = 0;
        activationZoneRotation.z = 0;
        transform.rotation = activationZoneRotation;
    }
}
