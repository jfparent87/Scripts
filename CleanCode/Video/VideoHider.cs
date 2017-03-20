using UnityEngine;
using System.Collections;

public class VideoHider : MonoBehaviour
{

    public bool isCreated = false;
    public GameObject video;
    public float proximityPlay = 2.5f;
    public float proximityStop = 3f;
    public float distance;
    public bool checkDistance;

    private RoomManager roomManager;

    void Start()
    {
        roomManager = GetComponentInParent<RoomManager>();
        if (video.GetComponent<Hider>().previousSize.x == 0.0f)
        {
            video.GetComponent<Hider>().previousSize = new Vector3(0.08f, 0.2f, 0.06f);
        }
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        checkDistance = true;
        video.GetComponent<VideoController>().pauseVideo();
        StartCoroutine(distanceCheck());
    }

    void OnSelect()
    {
        if (!isCreated)
        {
            instanciate();
        }
        else
        {
            destroy();
        }
    }

    private void Update()
    {
        if (roomManager.editionMode && !video.GetComponent<Hider>().showing)
        {
            video.GetComponent<Hider>().show();
        }
    }

    public void instanciate()
    {
        if (!isCreated)
        {
            video.GetComponent<Hider>().show();
            video.GetComponent<VideoController>().playVideo();
            isCreated = true;
        }
    }

    public void destroy()
    {
        if (isCreated)
        {
            video.GetComponent<VideoController>().pauseVideo();
            video.GetComponent<Hider>().hide();
            isCreated = false;
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distance < proximityPlay && !isCreated)
        {
            instanciate();
        }
        if (distance > proximityStop && isCreated)
        {
            destroy();
        }
    }

    IEnumerator distanceCheck()
    {
        Debug.Log("distance check for " + gameObject.name);
        while (checkDistance)
        {
            yield return new WaitForSeconds(2.0f);
            verifyDistance();
        }
    }
}