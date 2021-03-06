﻿using UnityEngine;

public class ActivationZoneTwo : MonoBehaviour
{

    public VideoController videoTwoController;
    public Hider videoTwoHider;
    public VideoHider videoHiderTwo;
    public VideoController videoThreeController;
    public Hider videoThreeHider;
    public VideoHider videoHiderThree;
    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public GameObject campfire;
    public ActivationZoneThree activationZoneThree;
    public bool collided;
    public bool videoThreeStarted;

    void Start()
    {
        videoThreeStarted = false;
        collided = false;
        videoThreeController.pauseVideo();
        videoThreeHider.hide();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            tapToPlaceCookingPot.nearFireThree = true;
            videoTwoController.pauseVideo();
            videoTwoHider.hide();
            videoHiderTwo.proximityPlay = 0.0f;
            collided = true;
            activationZoneThree.checkDistance = true;
        }
    }

    private void Update()
    {
        if (transform.position != campfire.transform.position)
        {
            transform.position = campfire.transform.position;
        }

        if (tapToPlaceCookingPot.onFireThreeAchieved && !videoThreeStarted)
        {
            videoHiderThree.instanciate();
            videoThreeController.playVideo();
            videoThreeHider.show();
            videoThreeStarted = true;
        }
    }
}
