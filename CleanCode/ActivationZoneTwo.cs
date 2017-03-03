using UnityEngine;

public class ActivationZoneTwo : MonoBehaviour
{

    public VideoController videoTwoController;
    public Hider videoTwoHider;
    public VideoHider videoHiderTwo;
    public VideoController videoThreeController;
    public Hider videoThreeHider;
    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public bool collided = false;
    public bool videoThreeStarted = false;

    void Start()
    {
        videoThreeController.stopVideo();
        videoThreeHider.hide();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            tapToPlaceCookingPot.nearFireThree = true;
            videoTwoController.stopVideo();
            videoTwoHider.hide();
            videoHiderTwo.proximityPlay = 0.0f;
            collided = true;
        }
    }

    private void Update()
    {
        if (tapToPlaceCookingPot.onFireThreeAchieved && !videoThreeStarted)
        {
            videoThreeController.playVideo();
            videoThreeHider.show();
            videoThreeStarted = true;
        }
    }
}
