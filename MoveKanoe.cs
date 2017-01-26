using UnityEngine;

public class MoveKanoe : MonoBehaviour
{
    public bool placing = false;
    public GameObject controls;
    public VideoInstanciator videoInstanciator;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
        }
    }

    // Update is called once per frame
    void Update()
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
                this.transform.position = hitInfo.point;
                controls.transform.position = hitInfo.point;
                videoInstanciator.resetPosition();
            }
        }
    }
}
