﻿using UnityEngine;

public class TapToPlaceGhost : MonoBehaviour
{

    public bool move = false;
    public float speed;
    public GameObject mainCamera;
    public GameObject ghostZone;
    public RoomManager roomManager;
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
        targetPosition = mainCamera.transform.position;
        ghostZonePosition = ghostZone.transform.position;
    }

    void moveToUser()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.2f));
        targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        Vector3 newSize = new Vector3(transform.localScale.x * 8.0f, transform.localScale.y * 8.0f, transform.localScale.z * 8.0f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, newSize, step);

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

        Vector3 newSize = new Vector3(transform.localScale.x / 8.0f, transform.localScale.y / 8.0f, transform.localScale.z / 8.0f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, newSize, step);

        Quaternion toQuat = GetComponentInParent<Transform>().rotation;
        this.transform.rotation = toQuat;
    }

    void SetLocalScale()
    {

        var newSize = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z * 1.5f);

        while (Vector3.Distance(transform.root.localScale, newSize) > 0)
        {
            transform.root.localScale = Vector3.MoveTowards(transform.root.localScale, newSize, Time.deltaTime * 10);
        }
    }
}