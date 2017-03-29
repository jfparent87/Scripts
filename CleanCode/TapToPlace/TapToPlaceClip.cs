using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;

public class TapToPlaceClip : MonoBehaviour
{
    public bool placing = false;
    public VideoHider videoHider;
    public float speed = 0.5f;
    public float distanceToCameraWhenPlacing = 1.2f;
    public GameObject videoAnchor;

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

    void OnSelect()
    {
        placing = !placing;

        if (placing && roomManager.editionMode)
        {
            freeAnchor();
        }
        if (!placing && roomManager.editionMode)
        {
            videoAnchor.transform.position = gameObject.transform.parent.position;
            lockAnchor();
        }
    }

    void Update()
    {
        if (placing)
        {
            if (videoHider.isCreated)
            {
                videoHider.videoScreen.GetComponent<VideoController>().pauseVideo();
            }
            
            if (!videoHider.isCreated) {
                videoHider.instanciate();
                videoHider.videoScreen.GetComponent<VideoController>().pauseVideo();
            }

            placeClipInFrontOfCamera();
        }
    }

    public void freeAnchor()
    {
        anchorManager.RemoveAnchor(videoAnchor);
    }

    public void lockAnchor()
    {
        videoAnchor.transform.position = this.transform.parent.transform.position;
        anchorManager.AttachAnchor(videoAnchor, videoAnchor.GetComponent<VideoAnchor>().SavedAnchorFriendlyName);
    }

    private void placeClipInFrontOfCamera()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
            30.0f, SpatialMapping.PhysicsRaycastMask))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
            step = speed * Time.deltaTime;
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, step);

            clipRotation = Camera.main.transform.localRotation;
            clipRotation.x = 0;
            clipRotation.z = 0;
            this.transform.parent.rotation = clipRotation;
        }
    }

}
