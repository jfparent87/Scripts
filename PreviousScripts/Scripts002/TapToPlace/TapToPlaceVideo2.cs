using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceVideo2 : TapToPlace {

    public VideoInstanciator2 videoInstanciator;

    public override void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
            //anchorManager.RemoveAnchor(this.transform.parent.gameObject);
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
            //anchorManager.AttachAnchor(this.transform.parent.gameObject, parentAnchor.SavedAnchorFriendlyName);
        }
    }

    public override void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
            if (videoInstanciator.isCreated)
            {
                videoInstanciator.instantiatedObject.GetComponent<VideoController>().stopVideo();
            }

            if (!videoInstanciator.isCreated)
            {
                videoInstanciator.instantiateToPlace();
                videoInstanciator.instantiatedObject.GetComponent<VideoController>().stopVideo();
            }
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.parent.position = hitInfo.point;

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
    }
}
