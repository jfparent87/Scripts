using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class VideoAnchor : MonoBehaviour
{
    bool placing = false;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";
    public TextMesh hololensDebugLine1;
    public TextMesh hololensDebugLine2;

    void Start()
    {
        // Make sure we have all the components in the scene we need.
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
        anchorManager.RemoveAnchor(this.gameObject);
    }

    public void lockAnchor()
    {
           anchorManager.AttachAnchor(this.gameObject, SavedAnchorFriendlyName);
    }
}
