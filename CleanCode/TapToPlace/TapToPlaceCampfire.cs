using UnityEngine;

public class TapToPlaceCampfire : MonoBehaviour
{
    bool placing = false;

    void OnSelect()
    {
        placing = !placing;
    }

    void Update()
    {
        if (placing)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                this.transform.position = hitInfo.point;
            }
        }
    }
}
