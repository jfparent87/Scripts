using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class GhostZone : MonoBehaviour {

    public GameObject ghostObject;
    public GameObject hololensCamera;
    public float speed = 0.5f;
    public RoomManager roomManager;
    private Vector3 targetPosition;
    private bool placing = false;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;

    void Start()
    {
        anchorManager = WorldAnchorManager.Instance;
        if (anchorManager == null)
        {
            Debug.LogError("This script expects that you have a WorldAnchorManager component in your scene.");
        }

        spatialMappingManager = SpatialMappingManager.Instance;
        if (spatialMappingManager == null)
        {
            Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
        }

        targetPosition = hololensCamera.transform.position;
        if (!roomManager.editionMode)
        {
            this.GetComponentInChildren<Hider>().hide();
        }
        else
        {
            this.GetComponentInChildren<Hider>().show();
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
                lockAnchor();
                GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
            }
        }
    }

    void Update()
    {
        if (placing && roomManager.editionMode)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
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

    public void freeAnchor()
    {
        anchorManager.RemoveAnchor(this.transform.gameObject);
    }

    public void lockAnchor()
    {
        anchorManager.AttachAnchor(this.transform.gameObject, this.GetComponent<GhostAnchor>().SavedAnchorFriendlyName);
    }
}
