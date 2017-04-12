using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceCampfire : MonoBehaviour
{

    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public GameObject campfireAnchor;
    public GameObject activationZone;
    public bool placing;
    public float speed = 1.5f;
    public Camera mainCamera;
    public float distanceToCameraWhenPlacing = 1.2f;

    private RoomManager roomManager;
    private Vector3 targetPosition;

    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    private float heightCorrection = 1.5f;
    private float step;

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

        roomManager = GetComponentInParent<RoomManager>();
        placing = false;
        GetComponent<MeshRenderer>().enabled = false;
        targetPosition = mainCamera.transform.position;
    }

    void OnSelect()
    {
        if (roomManager.editionMode)
        {
            placing = !placing;
            if (placing)
            {
                freeAnchor();
            }

            if (!placing)
            {
                if (activationZone)
                {
                    activationZone.transform.position = transform.position;
                }
                lockAnchor();
                tapToPlaceCookingPot.resetTargetFireTwo();
                tapToPlaceCookingPot.resetTargetFireThree();
            }
        }
    }

    void Update()
    {
        if (transform.position != campfireAnchor.transform.position && !placing)
        {
            transform.position = campfireAnchor.transform.position;
        }

        if (placing)
        {
            placeCampfireInFrontOfCamera();
        }
    }

    public void freeAnchor()
    {
        anchorManager.RemoveAnchor(campfireAnchor);
    }

    public void lockAnchor()
    {
        anchorManager.AttachAnchor(campfireAnchor, campfireAnchor.GetComponent<CampfireAnchor>().SavedAnchorFriendlyName);
    }

    private void placeCampfireInFrontOfCamera()
    {
        targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, mainCamera.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
