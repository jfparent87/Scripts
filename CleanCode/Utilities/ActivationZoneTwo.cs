using UnityEngine;

public class ActivationZoneTwo : MonoBehaviour
{

    public VideoController videoTwoController;
    public Hider videoTwoHider;
    public VideoHider videoHiderTwo;
    public VideoController videoThreeController;
    public Hider videoThreeHider;
    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public GameObject campfire;
    public ActivationZoneThree activationZoneThree;
    private bool collided;
    private bool videoThreeStarted;

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
            videoThreeHider.previousSize = new Vector3(0.08f, 0.2f, 0.06f);
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
            videoThreeController.playVideo();
            videoThreeHider.show();
            videoThreeStarted = true;
        }
    }
}
