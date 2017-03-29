using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class ActivationZoneAnchor : MonoBehaviour
{

    public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";

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

        if (anchorManager != null && spatialMappingManager != null)
        {
            anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
        }
        else
        {
            Destroy(this);
        }
    }

    public void freeAnchor()
    {
        anchorManager.RemoveAnchor(gameObject);
    }

    public void lockAnchor()
    {
        anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
    }
}
