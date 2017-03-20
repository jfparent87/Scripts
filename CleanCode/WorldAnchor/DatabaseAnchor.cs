using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
public class DatabaseAnchor : MonoBehaviour {

    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";
    public char videoNumber;

    void Start()
    {
        SavedAnchorFriendlyName = gameObject.name;
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

        getAnchorName();

        if (anchorManager != null && spatialMappingManager != null)
        {
            anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
        }

    }

    private void getAnchorName()
    {
        if (WorldAnchorManager.Instance.AnchorStore != null)
        {
            var ids = WorldAnchorManager.Instance.AnchorStore.GetAllIds();

            if (ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    if (id[0] == 'D' && id[1] == 'B' && id[2] == 'V' && id[3] == videoNumber)
                    {
                        Debug.Log(id);
                        SavedAnchorFriendlyName = id;
                    }
                }
            }
        }
    }
}
