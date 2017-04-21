using UnityEngine;

public class GhostZone : MonoBehaviour {

    public GameObject ghostObject;
    public float speed = 0.5f;
    public float distanceToCameraWhenPlacing = 1.2f;
    public GhostAnchor ghostAnchor;
    public bool placing = false;

    private RoomManager roomManager;
    private Vector3 targetPosition;
    private float heightCorrection = 1.5f;
    private float step;

    void Start()
    {
        targetPosition = Camera.main.transform.position;
        roomManager = GetComponentInParent<RoomManager>();

        if (!roomManager.editionMode)
        {
            GetComponentInChildren<Hider>().hide();
        }
        else
        {
            GetComponentInChildren<Hider>().show();
        }
    }

    public void selected()
    {

        if (!ghostObject.GetComponent<Hider>().showing && !roomManager.editionMode)
        {
            GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
            ghostObject.GetComponent<Hider>().previousSize = new Vector3(0.1f, 0.1f, 0.1f);
            ghostObject.GetComponent<Hider>().show();
            ghostObject.GetComponent<TapToPlaceGhost>().move = true;
        }

        if (roomManager.editionMode)
        {
            placing = !placing;

            if (placing)
            {
                freeAnchor();
            }

            if (!placing)
            {
                ghostAnchor.resetPosition(gameObject.transform.position);
                GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
                lockAnchor();     
            }
        }
    }

    void Update()
    {
        if (placing && roomManager.editionMode)
        {
            placeGhostZone();
        }

        if (roomManager.editionMode && ghostObject.GetComponent<Hider>().previousSize.x <= 0.1f)
        {
            ghostObject.GetComponent<Hider>().previousSize = ghostObject.GetComponent<TapToPlaceGhost>().objectScale;
        }
    }

    public void freeAnchor()
    {
        ghostAnchor.freeAnchor();
    }

    public void lockAnchor()
    {
        ghostAnchor.lockAnchor();
    }

    private void resetRotation()
    {
        Quaternion ghostZoneRotation = Camera.main.transform.localRotation;
        ghostZoneRotation.x = 0;
        ghostZoneRotation.z = 0;
        ghostZoneRotation *= Quaternion.Euler(0, 180f, 0);
        transform.rotation = ghostZoneRotation;
    }

    private void placeGhostZone()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
            30.0f, SpatialMapping.PhysicsRaycastMask))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            resetRotation();
        }
    }
}
