using UnityEngine;

public class TapToPlaceIngredient : MonoBehaviour
{
    public bool placing = false;
    public float speed;
    public GameObject mainCamera;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = mainCamera.transform.position;
    }

    void OnSelect()
    {
        placing = !placing;
    }

    void Update()
    {
        if (placing)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + 1.2f, Camera.main.nearClipPlane + 1.0f));
            targetPosition.Set(targetPosition.x, targetPosition.y + 0.02f, targetPosition.z);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.rotation = toQuat;
        }
    }
}

