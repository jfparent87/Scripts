using UnityEngine;

public class TapToPlaceGhost : MonoBehaviour
{

    // TODO a simplifier
    public bool placing = false;
    public Transform target;
    public Transform ghostZoneTarget;
    public float speed;
    public GameObject mainCamera;
    public GameObject ghostZone;
    private Vector3 targetPosition;
    private Vector3 ghostZonePosition;
    public bool targetPositionAchieved;
    public bool targetPositionAchievedOnce = false;

    private void Start()
    {
        resetTargetPosition();
    }

    void OnSelect()
    {
        placing = !placing;
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

        if (placing && !targetPositionAchieved)
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

        if (placing && targetPositionAchieved)
        {
            targetPositionAchievedOnce = true;
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.2f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            transform.Rotate(Vector3.up, speed * 10 * Time.deltaTime);
        }

        if (!placing && transform.position != ghostZonePosition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, ghostZonePosition, step);
            Quaternion toQuat = GetComponentInParent<Transform>().rotation;
            this.transform.rotation = toQuat;
        }

        if (!placing && transform.position  == ghostZonePosition)
        {
            GetComponent<Hider>().hide();
            targetPositionAchieved = false;
            targetPositionAchievedOnce = false;
        }
    }

    public void resetTargetPosition()
    {
        target = mainCamera.transform;
        targetPosition = target.position;
        ghostZoneTarget = ghostZone.transform;
        ghostZonePosition = ghostZoneTarget.position;
    }
}