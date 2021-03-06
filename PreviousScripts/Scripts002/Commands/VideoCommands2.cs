﻿using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class VideoCommands2 : Commands {
    public VideoAnchor parentAnchor;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;

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
    }

    public override void Update()
    {
        if (videoInstanciator.instantiatedObject != null && follow)
        {
            //anchorManager.RemoveAnchor(this.transform.parent.gameObject);
            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            videoInstanciator.instantiatedObject.transform.parent.rotation = toQuat;
            //anchorManager.AttachAnchor(this.transform.parent.gameObject, parentAnchor.SavedAnchorFriendlyName);
        }
    }
}
