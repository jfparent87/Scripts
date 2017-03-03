using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;

public class TapToPlaceClip : MonoBehaviour
{
    public bool placing = false;
    public VideoHider videoHider;
    public GameObject hololensCamera;
    public float speed = 0.5f;
    public RoomManager roomManager;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = hololensCamera.transform.position;
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
            lockAnchor();
        }
    }

    void Update()
    {
        if (placing)
        {
            if (videoHider.isCreated)
            {
                videoHider.video.GetComponent<VideoController>().stopVideo();
            }
            
            if (!videoHider.isCreated) {
                videoHider.instanciate();
                videoHider.video.GetComponent<VideoController>().stopVideo();
            }

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.5f, Camera.main.nearClipPlane + 1.0f));
                targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
                float step = speed * Time.deltaTime;
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, step);

                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
    }

    public void freeAnchor()
    {
        anchorManager.RemoveAnchor(this.transform.parent.gameObject);
    }

    public void lockAnchor()
    {
        anchorManager.AttachAnchor(this.transform.parent.gameObject, this.GetComponentInParent<VideoAnchor>().SavedAnchorFriendlyName);
    }

    IEnumerator waitAndFreeAnchor()
    {
        yield return new WaitForSeconds(0.5f);
        freeAnchor();
    }
}
