﻿using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceClip : MonoBehaviour
{
    public bool placing = false;
    public VideoHider videoHider;
    public float speed = 0.5f;
    public float distanceToCameraWhenPlacing = 1.2f;
    public GameObject videoAnchor;
    public GameObject thisVideo;

    private RoomManager roomManager;
    private Vector3 targetPosition;
    private float heightCorrection = 1.5f;
    private float step;
    private Quaternion clipRotation;

    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;

    void Start()
    {
        anchorManager = WorldAnchorManager.Instance;
        if (anchorManager == null)
        {
            Debug.LogError("This script expects that you have a WorldAnchorManager component in your scene.");
        }

        spatialMappingManager = SpatialMappingManager.Instance;
        if (spatialMappingManager == null)
        {
            Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
        }

        targetPosition = Camera.main.transform.position;
        roomManager = GetComponentInParent<RoomManager>();
    }

    public void OnSelect()
    {
        if (roomManager.editionMode)
        {
            placing = !placing;
            if (placing)
            {
                freeAnchor();
            }
            else
            {
                videoAnchor.GetComponent<VideoAnchor>().resetPosition(transform.position);
                lockAnchor();
            }
        }
    }

    void Update()
    {
        if (placing)
        {
            if (videoHider.isCreated)
            {
                videoHider.videoScreen.GetComponent<VideoPlayerController>().pauseVideo();
            }
            
            if (!videoHider.isCreated) {
                videoHider.instanciate();
                videoHider.videoScreen.GetComponent<VideoPlayerController>().pauseVideo();
            }

            placeClipInFrontOfCamera();
        }
    }

    public void freeAnchor()
    {
        videoAnchor.GetComponent<VideoAnchor>().freeAnchor();
    }

    public void lockAnchor()
    {
        videoAnchor.transform.position = thisVideo.transform.position;
        videoAnchor.GetComponent<VideoAnchor>().lockAnchor();
    }

    private void placeClipInFrontOfCamera()
    {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
            step = speed * Time.deltaTime;
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, step);

            clipRotation = Camera.main.transform.localRotation;
            clipRotation.x = 0;
            clipRotation.z = 0;
            transform.parent.rotation = clipRotation;
    }

}
