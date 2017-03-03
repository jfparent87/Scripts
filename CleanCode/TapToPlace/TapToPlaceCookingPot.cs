using UnityEngine;
using System.Collections.Generic;

public class TapToPlaceCookingPot : MonoBehaviour
{

    public GameObject fireTwo;
    public GameObject fireThree;
    public bool placing = false;
    public List<Ingredients> ingredients;
    public GameObject mainCamera;
    public float speed;
    public bool nearFireTwo = false;
    public bool nearFireThree = false;
    public bool onFireTwoAchieved = false;
    public bool onFireThreeAchieved = false;
    public bool locked = false;
    public Vector3 targetPosition;
    public Vector3 fireTwoPosition;
    public Vector3 fireThreePosition;

    private void Start()
    {
        targetPosition = mainCamera.transform.position;
        resetTargetFireTwo();
        resetTargetFireThree();
    }

    void OnSelect()
    {
        if (!locked)
        {
            placing = !placing;
            if (!placing)
            {
                for (int ingredient = 0; ingredient < ingredients.Count; ingredient++)
                {
                    ingredients[ingredient].resetTarget();
                }
            }
        }
    }

    void Update()
    {
        if (transform.position == fireTwoPosition)
        {
            onFireTwoAchieved = true;
            if (ingredients.Count == 0)
            {
                locked = false;
            }
        }

        if (transform.position == fireThreePosition)
        {
            onFireThreeAchieved = true;
        }

        if (placing)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.5f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.rotation = toQuat;
        }

        if (nearFireTwo && !onFireTwoAchieved)
        {
            placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, fireTwoPosition, step);
            locked = true;
        }

        if (nearFireThree && !onFireThreeAchieved)
        {
            placing = false;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, fireThreePosition, step);
            locked = true;
        }
    }

    public void resetTargetFireTwo()
    {
        fireTwoPosition = fireTwo.transform.position;
        fireTwoPosition.Set(fireTwoPosition.x, fireTwoPosition.y + 0.05f, fireTwoPosition.z);
    }

    public void resetTargetFireThree()
    {
        fireThreePosition = fireThree.transform.position;
        fireThreePosition.Set(fireThreePosition.x, fireThreePosition.y + 0.05f, fireThreePosition.z);
    }
}
