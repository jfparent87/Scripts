using UnityEngine;
using System.Collections;

public class Ingredients : MonoBehaviour {

    public GameObject cookingPot;
    public float speed = 0.2f;
    private bool nearCookingPot = false;
    private bool overCookingPotAchieved = false;
    private bool insideCookingPotAchieved = false;
    private Transform target;
    private Vector3 overCookingPot;
    private Vector3 insideCookingPot;
    private TapToPlaceIngredient ingredientPlacement;
    private bool audioPlayed = false;

    void Start()
    {
        ingredientPlacement = GetComponent<TapToPlaceIngredient>();
        resetTarget();
    }

    public void resetTarget()
    {
        target = cookingPot.transform;
        overCookingPot = target.position;
        overCookingPot.Set(overCookingPot.x, overCookingPot.y + 0.3f, overCookingPot.z);
        insideCookingPot = target.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            resetTarget();
            nearCookingPot = true;
        }
    }

    private void Update()
    {
        if (transform.position == overCookingPot)
        {
            overCookingPotAchieved = true;
        }

        if (transform.position == insideCookingPot)
        {
            insideCookingPotAchieved = true;
        }

        if (nearCookingPot && !overCookingPotAchieved)
        {
            ingredientPlacement.placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, overCookingPot, step);
        }

        if (nearCookingPot && overCookingPotAchieved && !insideCookingPotAchieved)
        {
            ingredientPlacement.placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, insideCookingPot, step);
            if (!audioPlayed)
            {
                this.GetComponent<AudioSource>().Play();
                audioPlayed = true;
            }
        }

        if (nearCookingPot && insideCookingPotAchieved)
        {
            nearCookingPot = false;
            StartCoroutine(waitAndDestroy());
        }
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        cookingPot.GetComponent<TapToPlaceCookingPot>().ingredients.Remove(this);
        Destroy(this.gameObject);
    }
}
