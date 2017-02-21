using UnityEngine;

public class GhostZone : MonoBehaviour {

    public GameObject ghost;
    bool placing = false;
    public bool firstPlacement;
    public Transform target;
    public GameObject mainCamera;
    private Vector3 targetPosition;
    public float speed = 0.5f;
    private int selectCounter = 0;


    void Start()
    {
        target = mainCamera.transform;
        targetPosition = target.position;
        firstPlacement = true;
        this.GetComponentInChildren<Hider>().hide();
    }

    void OnSelect()
    {

        if (!ghost.GetComponent<Hider>().showing && !firstPlacement)
        {
            GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
            ghost.GetComponent<Hider>().previousSize = new Vector3(0.8f, 0.8f, 0.8f);
            ghost.GetComponent<Hider>().show();
            ghost.GetComponent<TapToPlaceGhost>().placing = true;
        }

        selectCounter++;
        if (selectCounter == 2)
        {
            firstPlacement = false;
            GetComponentInChildren<TapToPlaceGhost>().resetTargetPosition();
        }

        if (firstPlacement)
        {
            placing = !placing;
        }
    }

    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing && firstPlacement)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
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
