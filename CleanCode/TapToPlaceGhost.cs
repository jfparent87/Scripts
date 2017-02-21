using UnityEngine;

public class TapToPlaceGhost : MonoBehaviour
{

    public bool move = false;
    public float speed;
    public GameObject mainCamera;
    public GameObject ghostZone;
    private Vector3 targetPosition;
    private Vector3 ghostZonePosition;
    private bool targetPositionAchieved;
    private bool targetPositionAchievedOnce = false;
    private const float ROTATION_SPEED = 10.0F;

    private void Start()
    {
        resetTargetPosition();
    }

    void OnSelect()
    {
        move = !move;
    }

    void Update()
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
            transform.Rotate(Vector3.up, speed * ROTATION_SPEED * Time.deltaTime);
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

    public void resetTargetPosition()
    {
        targetPosition = mainCamera.transform.position;
        ghostZonePosition = ghostZone.transform.position;
    }

    void moveToUser()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.2f));
        targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (!targetPositionAchievedOnce)
        {
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            toQuat *= Quaternion.Euler(0, 180f, 0);
            this.transform.rotation = toQuat;
        }
    }

    void moveToGhostZone()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ghostZonePosition, step);
        Quaternion toQuat = GetComponentInParent<Transform>().rotation;
        this.transform.rotation = toQuat;
    }
}