﻿using UnityEngine;

public class VideoHider : MonoBehaviour
{

    public bool isCreated = false;
    public GameObject video;
    public float proximityPlay = 2.5f;
    public float proximityStop = 3f;
    public float distance;

    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        InvokeRepeating("verifyDistance", 2.0f, 2.0f);
        video.GetComponent<VideoController>().movie.Stop();
        video.GetComponent<Hider>().hide();
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
            video.GetComponent<VideoController>().movie.Play();
            isCreated = true;
        }
    }

    public void destroy()
    {
        if (isCreated)
        {
            video.GetComponent<VideoController>().movie.Stop();
            video.GetComponent<Hider>().hide();
            isCreated = false;
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < proximityPlay && !isCreated)
        {
            Debug.Log("instanciate");
            instanciate();
        }
        if (distance > proximityStop && isCreated)
        {
            destroy();
        }
    }
}