using UnityEngine;

public class TapToPlaceActivationZoneThree : MonoBehaviour {

    public bool placing = false;
    public float speed;
    public GameObject mainCamera;
    public RoomManager roomManager;
    private Vector3 targetPosition;
    private ActivationZoneAnchor anchor;

    private void Start()
    {
        targetPosition = mainCamera.transform.position;
        anchor = GetComponent<ActivationZoneAnchor>();
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
        if (placing)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.0f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.rotation = toQuat;
        }
    }
}
