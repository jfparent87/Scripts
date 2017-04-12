using UnityEngine;
using System.Collections;

public class Ingredients : MonoBehaviour {

    public GameObject cookingPot;
    public GameObject whiteSmoke;
    public float speed = 0.2f;
    public bool locked = true;

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
        overCookingPot.Set(overCookingPot.x, overCookingPot.y + 0.25f, overCookingPot.z);
        insideCookingPot = target.position;
        insideCookingPot.Set(insideCookingPot.x, insideCookingPot.y + 0.1f, insideCookingPot.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone" && !locked)
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
            speed = 0.2f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, insideCookingPot, step);
            if (!audioPlayed)
            {
                whiteSmoke.GetComponent<ParticleSystem>().Play();
                GetComponent<AudioSource>().Play();
                audioPlayed = true;
            }
        }

        if (nearCookingPot && insideCookingPotAchieved)
        {
            nearCookingPot = false;
            StartCoroutine(waitAndHide());
        }
    }

    IEnumerator waitAndHide()
    {
        yield return new WaitForSeconds(0.5f);
        cookingPot.GetComponent<TapToPlaceCookingPot>().ingredients.Remove(this);
        whiteSmoke.GetComponent<ParticleSystem>().Stop();
        GetComponent<Hider>().hide();
    }
}
