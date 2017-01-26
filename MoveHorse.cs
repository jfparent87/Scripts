using UnityEngine;
using System.Collections;


public class MoveHorse : MonoBehaviour
{
    public Animator animator;

    void OnAnimatorMove()
    {
        if (animator)
        {
            Vector3 newPosition = transform.position;
            newPosition.z += 2 * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}