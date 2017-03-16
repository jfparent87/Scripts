using UnityEngine;

public class TapToPlaceIngredient : MonoBehaviour
{
    public bool placing;
    public float speed;
    public float distanceToCameraWhenPlacing = 1.0f;

    private Vector3 targetPosition;
    private float heightCorrection = 1.5f;
    private float step;

    private void Start()
    {
        placing = false;
        targetPosition = Camera.main.transform.position;
    }

    void OnSelect()
    {
        placing = !placing;
    }

    void Update()
    {
        if (placing)
        {
            placeIngredientInFrontOfCamera();
        }
    }

    private void placeIngredientInFrontOfCamera()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}

