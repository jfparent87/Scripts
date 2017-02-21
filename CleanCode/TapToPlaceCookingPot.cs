using UnityEngine;
using System.Collections.Generic;

public class TapToPlaceCookingPot : MonoBehaviour
{
    bool placing = false;
    public List<Ingredients> ingredients;

    void OnSelect()
    {
        placing = !placing;
        if(!placing)
        {
            for (int ingredient = 0; ingredient < ingredients.Count; ingredient++)
            {
                ingredients[ingredient].resetTarget();
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
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.rotation = toQuat;
            }
        }
    }
}
