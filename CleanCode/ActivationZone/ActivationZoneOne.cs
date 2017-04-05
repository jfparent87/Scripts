using UnityEngine;

public class ActivationZoneOne : MonoBehaviour {

    public VideoPlayerController videoOneController;
    public Hider videoOneHider;
    public VideoHider videoHiderOne;
    public VideoHider videoHiderTwo;
    public VideoPlayerController videoTwoController;
    public Hider videoTwoHider;
    public GameObject campfire;
    public bool collided = false;
    public bool videoTwoStarted = false;
    public RoomManager roomManager;

    private TapToPlaceCookingPot tapToPlaceCookingPot;

    void Start () {
        tapToPlaceCookingPot = transform.parent.parent.GetComponentInChildren<TapToPlaceCookingPot>();
        videoTwoController.pauseVideo();
        videoTwoHider.hide();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone" && !roomManager.editionMode)
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
