using UnityEngine;
using System.Collections;

public class TapToPlaceGhost : MonoBehaviour
{

    public bool move = false;
    public float speed;
    public GameObject ghostZone;
    public float distanceToCameraWhenPlacing = 0.8f;
    public Vector3 objectScale;
    public RoomManager roomManager;
    public GameObject ghostSmoke;
    public GameObject ghostAnchor;
    public float showingTime = 15.0f;

    private Vector3 targetPosition;
    public Vector3 ghostZonePosition;
    public bool targetPositionAchieved;
    private bool targetPositionAchievedOnce = false;
    private const float rotationSpeed = 10.0F;
    private Vector3 ghostObjectScale;
    private float heightCorrection = 1.5f;
    private float step;
    public bool waitingToGoBack = false;
    private bool moving = false;

    private void Start()
    {
        resetTargetPosition();
        resetPosition();
        scaleToNormalSize();
        roomManager = GetComponentInParent<RoomManager>();
    }

    public void OnSelect()
    {
        if (roomManager.editionMode)
        {
            ghostZone.GetComponent<GhostZone>().selected();
        }
        else
        {
            if (!moving)
            {
                move = !move;
            }

        }
    }

    void Update()
    {
        if (gameObject.transform.position != ghostAnchor.transform.position && !move && !moving && !ghostZone.GetComponent<GhostZone>().placing
            && !waitingToGoBack)
        {
            gameObject.transform.position = ghostAnchor.transform.position;
        }

        if (!roomManager.editionMode)
        {
            if (transform.position == targetPosition)
            {
                targetPositionAchieved = true;
                moving = false;
                ghostSmoke.SetActive(false);
            }

            if (transform.position == ghostZonePosition && !move)
            {
                targetPositionAchieved = false;
                moving = false;
                ghostSmoke.SetActive(true);
            }

            if (move && !targetPositionAchieved)
            {
                moving = true;
                moveToUser();
            }

            if (move && targetPositionAchieved)
            {
                targetPositionAchievedOnce = true;
                transform.Rotate(Vector3.up, speed * rotationSpeed * Time.deltaTime);
                if (!waitingToGoBack)
                {
                    StartCoroutine(waitAndGoBackInPlace());
                }
            }

            if (!move && transform.position != ghostZonePosition)
            {
                moving = true;
                moveToGhostZone();
            }

            if (!move && transform.position == ghostZonePosition)
            {
                GetComponent<Hider>().hide();
                targetPositionAchieved = false;
                targetPositionAchievedOnce = false;
            }

            if (GetComponent<Hider>().previousSize != new Vector3(0.8f, 0.8f, 0.8f))
            {
                GetComponent<Hider>().previousSize = new Vector3(0.8f, 0.8f, 0.8f);
            }

        }
        else
        {
            if (!GetComponent<Hider>().showing)
            {
                GetComponent<Hider>().show();
            }
            if (gameObject.transform.localScale != objectScale)
            {
                gameObject.transform.localScale = objectScale;
            }
        }
    }

    public void resetTargetPosition()
    {
        targetPosition = Camera.main.transform.position;
        ghostZonePosition = ghostAnchor.transform.position;
        transform.position = ghostZonePosition;
    }

    void moveToUser()
    {
        scaleToNormalSize();
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        transform.localScale = Vector3.MoveTowards(transform.localScale, ghostObjectScale, step * 3.0f);
        
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
        transform.localScale = Vector3.MoveTowards(transform.localScale, ghostObjectScale, step * 3.0f);

        Quaternion ghostObjectRotation = GetComponentInParent<Transform>().rotation;
        transform.rotation = ghostObjectRotation;

        waitingToGoBack = false;
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

    IEnumerator waitAndGoBackInPlace()
    {
        waitingToGoBack = true;
        yield return new WaitForSeconds(showingTime);
        if (gameObject.transform.position != ghostZonePosition && waitingToGoBack)
        {
            move = !move;
        }
    }

    public void resetPosition()
    {
        gameObject.transform.position = ghostAnchor.transform.position;
    }
}