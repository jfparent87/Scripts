using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class CampfireAnchor : MonoBehaviour
{
    
    public GameObject campfire;
    public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";

    private bool placing = false;
    private RoomManager roomManager;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;

    void Start()
    {

        roomManager = GetComponentInParent<RoomManager>();
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

        if (anchorManager != null && spatialMappingManager != null)
        {
            anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
        }

    }

    private void Update()
    {
        if (roomManager.editionMode && transform.position != campfire.transform.position)
        {
            transform.position = campfire.transform.position;
        }
    }
}
