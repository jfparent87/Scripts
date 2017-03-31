using UnityEngine;
using System.Collections;

public class VideoHider : MonoBehaviour
{

    public bool isCreated = false;
    public GameObject videoScreen;
    public GameObject video;
    public float proximityStop = 3f;
    public float distance;
    public bool checkDistance;
    public VideoAnchor videoAnchor;

    private RoomManager roomManager;

    void Start()
    {
        roomManager = GetComponentInParent<RoomManager>();
        if (videoScreen.GetComponent<Hider>().previousSize.x == 0.0f)
        {
            videoScreen.GetComponent<Hider>().previousSize = new Vector3(0.08f, 0.2f, 0.06f);
        }
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        checkDistance = true;
        videoScreen.GetComponent<VideoController>().pauseVideo();
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
        if (roomManager.editionMode && !videoScreen.GetComponent<Hider>().showing)
        {
            videoScreen.GetComponent<Hider>().show();
        }
    }

    public void instanciate()
    {
        videoAnchor.freeAnchor();
        Vector3 newPosition = new Vector3(video.transform.position.x, Camera.main.transform.position.y - 0.3f, video.transform.position.z);
        video.transform.position = newPosition;
        videoAnchor.transform.position = video.transform.position;
        videoAnchor.lockAnchor();
        if (!isCreated)
        {
            videoScreen.GetComponent<Hider>().show();
            videoScreen.GetComponent<VideoController>().playVideo();
            isCreated = true;
        }
    }

    public void destroy()
    {
        if (isCreated)
        {
            videoScreen.GetComponent<VideoController>().pauseVideo();
            videoScreen.GetComponent<Hider>().hide();
            isCreated = false;
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distance > proximityStop && isCreated && !roomManager.editionMode)
        {
            destroy();
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

    public IEnumerator waitAndInstanciate()
    {
        yield return new WaitForSeconds(3.0f);
        instanciate();
    }
}