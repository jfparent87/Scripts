﻿using UnityEngine;

public class ActivationZoneOne : MonoBehaviour {

    public VideoController videoOneController;
    public Hider videoOneHider;
    public VideoHider videoHiderOne;
    public VideoHider videoHiderTwo;
    public VideoController videoTwoController;
    public Hider videoTwoHider;
    public GameObject campfire;
    public bool collided = false;
    public bool videoTwoStarted = false;

    private TapToPlaceCookingPot tapToPlaceCookingPot;

    void Start () {
        tapToPlaceCookingPot = transform.parent.parent.GetComponentInChildren<TapToPlaceCookingPot>();
        videoTwoController.pauseVideo();
        videoTwoHider.hide();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            tapToPlaceCookingPot.nearFireTwo = true;
            videoOneController.pauseVideo();
            videoOneHider.hide();
            collided = true;
        }
    }

    private void Update()
    {
        if (transform.position != campfire.transform.position)
        {
            transform.position = campfire.transform.position;
        }

        if (tapToPlaceCookingPot.onFireTwoAchieved && !videoTwoStarted)
        {
            videoHiderTwo.instanciate();
            videoTwoController.playVideo();
            videoTwoHider.show();
            videoTwoStarted = true;
        }
    }
}