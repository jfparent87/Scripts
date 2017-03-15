﻿using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class CampfireAnchor : MonoBehaviour
{
    bool placing = false;
    public RoomManager roomManager;
    public GameObject campfire;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";

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

    private void Update()
    {
        if (roomManager.editionMode)
        {
            this.transform.position = campfire.transform.position;
        }
    }
}
