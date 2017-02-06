using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceObject : MonoBehaviour
{
    bool placing = false;
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
            // If we don't have what we need to proceed, we may as well remove ourselves.
            Destroy(this);
        }
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
            anchorManager.RemoveAnchor(gameObject);
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
            // Add world anchor when object placement is done.
            anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {

            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.position = hitInfo.point;

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.rotation = toQuat;
            }
        }
    }
}
