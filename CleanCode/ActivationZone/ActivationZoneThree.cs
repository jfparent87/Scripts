using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivationZoneThree : MonoBehaviour
{

    public float proximityTrigger = 2.5f;
    public bool checkDistance;
    public GameObject freeVisit;
    public List<GameObject> objectsToHide;
    public List<GameObject> ghostObjects;

    private bool videoThreeStarted;
    private float distance;
    private bool freeVisitActivated;
    private RoomManager roomManager;
    private MeshRenderer meshRendered;
    private bool checkDistanceStarted;

    void Start()
    {
        checkDistanceStarted = false;
        freeVisitActivated = false;
        videoThreeStarted = false;
        roomManager = GetComponentInParent<RoomManager>();
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        checkDistance = false;        
        meshRendered = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (roomManager.editionMode && !meshRendered.enabled)
        {
            meshRendered.enabled = true;
        }

        if (!roomManager.editionMode && meshRendered.enabled)
        {
            meshRendered.enabled = false;
        }

        if (checkDistance && !checkDistanceStarted)
        {
            checkDistanceStarted = true;
            StartCoroutine(distanceCheck());
        }

        if (roomManager.editionMode && !freeVisit.activeInHierarchy)
        {
            freeVisit.SetActive(true);
        }

        if (freeVisitActivated && !freeVisit.activeInHierarchy)
        {
            freeVisit.SetActive(true);
            foreach (var objectToHide in objectsToHide)
            {
                if (objectToHide.GetComponentInChildren<VideoPlayerController>())
                {
                    objectToHide.GetComponentInChildren<VideoPlayerController>().resetVideo();
                }
                objectToHide.GetComponent<Hider>().hide();
            }
            foreach (var ghostObject in ghostObjects)
            {
                ghostObject.GetComponent<Hider>().hide();
            }
        }

        if (!roomManager.editionMode && freeVisit.activeInHierarchy && !freeVisitActivated)
        {
            freeVisit.SetActive(false);
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distance < proximityTrigger)
        {
            freeVisitActivated = true;
            foreach (var ghostObject in ghostObjects)
            {
                ghostObject.GetComponent<TapToPlaceGhost>().resetTargetPosition();
            }
        }
    }

    IEnumerator distanceCheck()
    {
        while (checkDistance)
        {
            yield return new WaitForSeconds(2.0f);
            verifyDistance();
        }
    }
}
