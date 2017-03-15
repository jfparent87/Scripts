using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class WorldAnchorSwitch : MonoBehaviour
{
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    public List<GameObject> anchors;
    public List<string> SavedAnchorsFriendlyNames;
    public bool anchor = true;
    public RoomManager roomManager;

    void OnSelect()
    {
        roomManager.editionMode = !roomManager.editionMode;
    }

    /*
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
            StartCoroutine(wait());
        }
    }
    
    void OnSelect()
    {
        if (anchor)
        {
            freeAnchors();
        }
        else
        {
            lockAnchors();
        }
    }
    
    void freeAnchors()
    {
        for (int i = 0; i < anchors.Count; i++)
        {
            anchorManager.RemoveAnchor(anchors[i].gameObject);
        }
        anchor = false;
    }

    void lockAnchors()
    {
        for (int i = 0; i < anchors.Count; i++)
        {
            anchorManager.AttachAnchor(anchors[i].gameObject, SavedAnchorsFriendlyNames[i]);
        }
        anchor = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);
        lockAnchors();
    }
    */
}
