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

    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        checkDistance = true;
        video.GetComponent<VideoController>().stopVideo();
        video.GetComponent<Hider>().hide();
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

    public void instanciate()
    {
        if (!isCreated)
        {
            video.GetComponent<Hider>().previousSize = new Vector3(0.08f, 0.2f, 0.06f);
            video.GetComponent<Hider>().show();
            video.GetComponent<VideoController>().playVideo();
            isCreated = true;
        }
    }

    public void destroy()
    {
        if (isCreated)
        {
            video.GetComponent<VideoController>().stopVideo();
            video.GetComponent<Hider>().hide();
            isCreated = false;
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
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
        while (checkDistance)
        {
            yield return new WaitForSeconds(2.0f);
            verifyDistance();
        }
    }
}