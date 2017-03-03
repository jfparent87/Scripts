using UnityEngine;

public class TapToPlaceCampfire : MonoBehaviour
{

    bool placing = false;
    public RoomManager roomManager;
    public TapToPlaceCookingPot tapToPlaceCookingPot;

    void OnSelect()
    {
        if (roomManager.editionMode)
        {
            placing = !placing;
            if (!placing)
            {
                tapToPlaceCookingPot.resetTargetFireTwo();
                tapToPlaceCookingPot.resetTargetFireThree();
            }
        }
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
