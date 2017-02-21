using UnityEngine;
using System.Collections;

public class Ingredients : MonoBehaviour {

    public TapToPlaceObjectDistance ingredientPlacement;
    public GameObject cookingPot;
    public float speed = 0.2f;
    private Transform target;
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    public bool move = false;
    public bool position1Achieved = false;
    public bool position2Achieved = false;
    private bool audioPlayed = false;

    void Start()
    {
        resetTarget();
    }

    public void resetTarget()
    {
        target = cookingPot.transform;
        targetPosition1 = target.position;
        targetPosition1.Set(targetPosition1.x, targetPosition1.y + 0.22f, targetPosition1.z);
        targetPosition2 = target.position;
        targetPosition2.Set(targetPosition2.x, targetPosition2.y, targetPosition2.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "CookingPotTriggerZone")
        {
            move = true;
        }
    }

    private void Update()
    {
        if (transform.position == targetPosition1)
        {
            position1Achieved = true;
        }

        if (transform.position == targetPosition2)
        {
            position2Achieved = true;
        }

        if (move && !position1Achieved)
        {
            ingredientPlacement.placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition1, step);
        }

        if (move && position1Achieved && !position2Achieved)
        {
            ingredientPlacement.placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition2, step);
            if (!audioPlayed)
            {
                this.GetComponent<AudioSource>().Play();
                audioPlayed = true;
            }
        }

        if (move && position2Achieved)
        {
            move = false;
            StartCoroutine(waitAndDestroy());
        }
    }

    IEnumerator waitAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
