using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TapToPlaceCookingPot : MonoBehaviour
{

    public GameObject fireTwo;
    public GameObject fireTwoFlames;
    public GameObject fireThree;
    public bool placing;
    public List<Ingredients> ingredients;
    public bool ingredientsLocked;
    public float speed;
    public bool locked;
    public bool nearFireTwo;
    public bool nearFireThree;
    public bool onFireTwoAchieved;
    public bool onFireThreeAchieved;
    public float distanceToCameraWhenPlacing = 1.2f;
    public RoomManager roomManager;
    public CookingPotAnchor cookingPotAnchor;

    private GameObject fireOne;
    private Vector3 targetPosition;
    private Vector3 fireTwoPosition;
    private Vector3 fireThreePosition;
    private float heightCorrection = 1.5f;
    private float step;

    private void Start()
    {
        fireOne = GameObject.Find("CampfireOne");
        locked = true;
        ingredientsLocked = true;
        placing = false;
        nearFireTwo = false;
        nearFireThree = false;
        onFireTwoAchieved = false;
        onFireThreeAchieved = false;
        fireTwoPosition = 
        targetPosition = Camera.main.transform.position;
        resetTargetFireTwo();
        resetTargetFireThree();
        StartCoroutine(resetPosition());
    }

    void OnSelect()
    {
        resetTargetFireTwo();
        resetTargetFireThree();
        if (!locked || roomManager.editionMode)
        {
            placing = !placing;
            if (placing && roomManager.editionMode)
            {
                cookingPotAnchor.freeAnchor();
            }

            if (!placing)
            {
                foreach (var ingredient in ingredients)
                {
                    ingredient.resetTarget();
                }
                if (roomManager.editionMode)
                {
                    cookingPotAnchor.gameObject.transform.position = gameObject.transform.position;
                    cookingPotAnchor.lockAnchor();
                }
            }
        }
    }

    void Update()
    {
        if (placing)
        {
            placeCookingPotInFrontOfCamera();
        }

        if (!roomManager.editionMode)
        {
            if (!ingredientsLocked)
            {
                foreach (var ingredient in ingredients)
                {
                    ingredient.locked = false;
                }
            }
            if (transform.position == fireTwoPosition)
            {
                arrivedAtFireTwo();
                fireTwoFlames.SetActive(true);
            }

            if (transform.position == fireThreePosition)
            {
                onFireThreeAchieved = true;
            }

            if (nearFireTwo && !onFireTwoAchieved)
            {
                placeCookingPotOverFire(fireTwoPosition);
            }

            if (nearFireThree && !onFireThreeAchieved)
            {
                placeCookingPotOverFire(fireThreePosition);
            }

            if (ingredients.Count == 0 && fireTwoFlames.activeInHierarchy)
            {
                fireTwoFlames.GetComponentInChildren<ParticleSystem>().Stop();
            }
        }
    }

    public void resetTargetFireTwo()
    {
        fireTwoPosition = fireTwo.transform.position;
        fireTwoPosition.Set(fireTwoPosition.x, fireTwoPosition.y + 0.04f, fireTwoPosition.z);
    }

    public void resetTargetFireThree()
    {
        fireThreePosition = fireThree.transform.position;
        fireThreePosition.Set(fireThreePosition.x, fireThreePosition.y + 0.04f, fireThreePosition.z);
    }

    private void arrivedAtFireTwo()
    {
        onFireTwoAchieved = true;
        if (ingredients.Count == 0)
        {
            locked = false;
        }
    }

    IEnumerator resetPosition()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = cookingPotAnchor.gameObject.transform.position;
    }

    private void placeCookingPotInFrontOfCamera()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void placeCookingPotOverFire(Vector3 firePosition)
    {
        placing = false;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, firePosition, step);
        locked = true;
    }

}
