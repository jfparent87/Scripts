using UnityEngine;

public class TapToPlaceGhost : MonoBehaviour
{

    public bool move = false;
    public float speed;
    public GameObject ghostZone;
    public float distanceToCameraWhenPlacing = 1.2f;
    public Vector3 objectScale;

    private RoomManager roomManager;
    private Vector3 targetPosition;
    private Vector3 ghostZonePosition;
    private bool targetPositionAchieved;
    private bool targetPositionAchievedOnce = false;
    private const float rotationSpeed = 10.0F;
    private Vector3 ghostObjectScale;
    private float heightCorrection = 1.5f;
    private float step;

    private void Start()
    {
        resetTargetPosition();
        scaleToNormalSize();
        roomManager = GetComponentInParent<RoomManager>();
    }

    void OnSelect()
    {
        if (roomManager.editionMode)
        {
            ghostZone.GetComponent<GhostZone>().selected();
        }
        else
        {
            move = !move;
        }
    }

    void Update()
    {
        if (!roomManager.editionMode)
        {
            if (transform.position == targetPosition)
            {
                targetPositionAchieved = true;
            }
            else
            {
                targetPositionAchieved = false;
            }

            if (move && !targetPositionAchieved)
            {
                moveToUser();
            }

            if (move && targetPositionAchieved)
            {
                targetPositionAchievedOnce = true;
                transform.Rotate(Vector3.up, speed * rotationSpeed * Time.deltaTime);
            }

            if (!move && transform.position != ghostZonePosition)
            {
                moveToGhostZone();
            }

            if (!move && transform.position == ghostZonePosition)
            {
                GetComponent<Hider>().hide();
                targetPositionAchieved = false;
                targetPositionAchievedOnce = false;
            }
        }
        else
        {
            if (!GetComponent<Hider>().showing)
            {
                GetComponent<Hider>().show();
            }
        }
    }

    public void resetTargetPosition()
    {
        targetPosition = Camera.main.transform.position;
        ghostZonePosition = ghostZone.transform.position;
        transform.position = ghostZonePosition;
    }

    void moveToUser()
    {
        scaleToNormalSize();
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        transform.localScale = Vector3.MoveTowards(transform.localScale, ghostObjectScale, step);
        
        if (!targetPositionAchievedOnce)
        {
            resetRotation();
        }
    }

    void moveToGhostZone()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ghostZonePosition, step);

        scaleToSmallSize();
        transform.localScale = Vector3.MoveTowards(transform.localScale, ghostObjectScale, step * 0.8f);

        Quaternion ghostObjectRotation = GetComponentInParent<Transform>().rotation;
        this.transform.rotation = ghostObjectRotation;
    }

    private void scaleToNormalSize()
    {
        ghostObjectScale = objectScale;
    }

    private void scaleToSmallSize()
    {
        ghostObjectScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void resetRotation()
    {
        Quaternion ghostObjectRotation = Camera.main.transform.localRotation;
        ghostObjectRotation.x = 0;
        ghostObjectRotation.z = 0;
        ghostObjectRotation *= Quaternion.Euler(0, 180f, 0);
        this.transform.rotation = ghostObjectRotation;
    }
}