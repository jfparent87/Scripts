using UnityEngine;

public class GhostZone : MonoBehaviour {

    public GameObject ghostObject;
    public GameObject hololensCamera;
    public float speed = 0.5f;
    private bool placeGhostZone;
    private Vector3 targetPosition;
    private int selectCounter = 0;
    private bool placing = false;


    void Start()
    {
        targetPosition = hololensCamera.transform.position;
        placeGhostZone = true;
        this.GetComponentInChildren<Hider>().hide();
    }

    void OnSelect()
    {

        if (!ghostObject.GetComponent<Hider>().showing && !placeGhostZone)
        {
            GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
            ghostObject.GetComponent<Hider>().previousSize = new Vector3(0.8f, 0.8f, 0.8f);
            ghostObject.GetComponent<Hider>().show();
            ghostObject.GetComponent<TapToPlaceGhost>().move = true;
        }

        selectCounter++;
        if (selectCounter == 2)
        {
            placeGhostZone = false;
            GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
        }

        if (placeGhostZone)
        {
            placing = !placing;
        }
    }

    void Update()
    {
        if (placing && placeGhostZone)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.0f));
                targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.rotation = toQuat;
            }
        }
    }
}
