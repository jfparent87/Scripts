using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivationZoneThree : MonoBehaviour
{

    public bool videoThreeStarted = false;
    public float proximityTrigger = 2.5f;
    public float distance;
    public bool checkDistance;
    public RoomManager roomManager;
    private MeshRenderer meshRendered;
    private TextMesh textMesh;
    public GameObject freeVisit;
    public bool freeVisitActivated = false;
    private bool checkDistanceStarted = false;
    public List<GameObject> objectsToDeactivate;

    void Start()
    {
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
            foreach (var objectToDeactivate in objectsToDeactivate)
            {
                objectToDeactivate.SetActive(false);
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
