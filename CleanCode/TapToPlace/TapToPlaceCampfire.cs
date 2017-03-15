using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceCampfire : MonoBehaviour
{

    bool placing = false;
    public RoomManager roomManager;
    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public GameObject campfireAnchor;
    public GameObject activationZone;
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
                    activationZone.transform.position = this.transform.position;
                }
                lockAnchor();
                tapToPlaceCookingPot.resetTargetFireTwo();
                tapToPlaceCookingPot.resetTargetFireThree();
            }
        }
    }

    void Update()
    {
        if (this.transform.position != campfireAnchor.transform.position)
        {
            this.transform.position = campfireAnchor.transform.position;
        }

        if (placing)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                this.transform.position = hitInfo.point;
            }
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
}
