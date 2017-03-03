using UnityEngine;

public class ActivationZoneOne : MonoBehaviour {

    public VideoController videoOneController;
    public Hider videoOneHider;
    public VideoHider videoHiderOne;
    public VideoController videoTwoController;
    public Hider videoTwoHider;
    public TapToPlaceCookingPot tapToPlaceCookingPot;
    public bool collided = false;
    public bool videoTwoStarted = false;

	void Start () {
        videoTwoController.stopVideo();
        videoTwoHider.hide();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            tapToPlaceCookingPot.nearFireTwo = true;
            videoOneController.stopVideo();
            videoOneHider.hide();
            videoHiderOne.proximityPlay = 0.0f;
            collided = true;
        }
    }

    private void Update()
    {
        if (tapToPlaceCookingPot.onFireTwoAchieved && !videoTwoStarted)
        {
            videoTwoController.playVideo();
            videoTwoHider.show();
            videoTwoStarted = true;
        }
    }
}
