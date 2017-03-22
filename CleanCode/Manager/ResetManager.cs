using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour {

    public List<TapToPlaceGhost> ghostZones;
    public List<VideoController> videoControllers;
    //public List<Hider> hiders;
    public TapToPlaceCookingPot cookingPot;
    public List<TapToPlaceIngredient> ingredients;
    public ActivationZoneOne activationZoneOne;
    public ActivationZoneTwo activationZoneTwo;
    public ActivationZoneThree activationZoneThree;
    public VideoHider videoHiderOne;
    public GameObject freeVisit;

    public void OnSelect()
    {
        Debug.Log("Reset Scene");
        SceneManager.LoadScene("Scene002");

        foreach (var ghostZone in ghostZones)
        {
            ghostZone.resetTargetPosition();
        }

        /*foreach (var hider in hiders)
        {
            try
            {
                hider.show();
                Debug.Log("show: " + hider.gameObject.name);
            }
            catch
            {
                Debug.Log("didn't show: " + hider.gameObject.name);
            }

            if (hider.gameObject.name == "Meat" || hider.gameObject.name == "Mushroom")
            {
                hider.gameObject.GetComponent<TapToPlaceIngredient>().resetPosition();
            }
        }*/
        /*
        foreach (var videoController in videoControllers)
        {
            videoController.resetVideo();
            videoController.gameObject.GetComponent<Hider>().hide();
        }*/
        /*
        resetCookingPot();
        resetIngredients();
        resetActivationZoneOne();
        resetActivationZoneTwo();
        resetActivationZoneThree();
        videoHiderOne.proximityPlay = 2.75f;
        freeVisit.SetActive(false);
        */
    }

    private void resetCookingPot()
    {
        cookingPot.nearFireTwo = false;
        cookingPot.nearFireThree = false;
        cookingPot.locked = false;
        cookingPot.onFireTwoAchieved = false;
        cookingPot.onFireThreeAchieved = false;
    }

    private void resetActivationZoneOne()
    {
        activationZoneOne.collided = false;
        activationZoneOne.videoTwoStarted = false;
    }

    private void resetActivationZoneTwo()
    {
        activationZoneTwo.collided = false;
        activationZoneTwo.videoThreeStarted = false;
    }

    private void resetActivationZoneThree()
    {
        activationZoneThree.checkDistance = false;
    }

}
