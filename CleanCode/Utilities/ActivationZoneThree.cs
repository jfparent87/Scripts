using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivationZoneThree : MonoBehaviour
{

    public float proximityTrigger = 2.5f;
    public bool checkDistance;
    public GameObject freeVisit;
    public List<GameObject> objectsToHide;

    private bool videoThreeStarted;
    private float distance;
    private bool freeVisitActivated;
    private RoomManager roomManager;
    private MeshRenderer meshRendered;
    private TextMesh textMesh;
    private bool checkDistanceStarted;

    void Start()
    {
        checkDistanceStarted = false;
        freeVisitActivated = false;
        videoThreeStarted = false;
        roomManager = GetComponentInParent<RoomManager>();
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        checkDistance = false;        
        meshRendered = GetComponent<MeshRenderer>();
        textMesh = GetComponentInChildren<TextMesh>();
        if (!roomManager.editionMode)
        {
            textMesh.text = "";
        }
    }

    private void Update()
    {
        if (roomManager.editionMode && !meshRendered.enabled)
        {
            meshRendered.enabled = true;
            textMesh.text = "Activation Zone Three";
        }

        if (!roomManager.editionMode && meshRendered.enabled)
        {
            meshRendered.enabled = false;
            textMesh.text = "";
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
                if (objectToHide.GetComponentInChildren<VideoController>())
                {
                    objectToHide.GetComponentInChildren<VideoController>().resetVideo();
                }
                objectToHide.GetComponent<Hider>().hide();
            }
            
        }

        if (!roomManager.editionMode && freeVisit.activeInHierarchy && !freeVisitActivated)
        {
            freeVisit.SetActive(false);
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < proximityTrigger)
        {
            freeVisitActivated = true;
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
