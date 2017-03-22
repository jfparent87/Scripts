using UnityEngine;
using System.Collections;

public class TapToPlaceIngredient : MonoBehaviour
{
    public bool placing;
    public float speed;
    public float distanceToCameraWhenPlacing = 1.0f;
    public RoomManager roomManager;
    public CookingPotAnchor ingredientAnchor;

    private Vector3 targetPosition;
    private float heightCorrection = 1.5f;
    private float step;

    private void Start()
    {
        placing = false;
        targetPosition = Camera.main.transform.position;
        StartCoroutine(resetPosition());
    }

    void OnSelect()
    {
        placing = !placing;

        if (placing && roomManager.editionMode)
        {
            ingredientAnchor.freeAnchor();
        }

        if (!placing && roomManager.editionMode)
        {
            ingredientAnchor.gameObject.transform.position = gameObject.transform.position;
            ingredientAnchor.lockAnchor();
        }
    }

    void Update()
    {
        if (placing)
        {
            placeIngredientInFrontOfCamera();
        }
    }

    IEnumerator resetPosition()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = ingredientAnchor.gameObject.transform.position;
    }

    private void placeIngredientInFrontOfCamera()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}

