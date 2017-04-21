using UnityEngine;
using System.Collections;

public class GhostAnchorPosition : MonoBehaviour
{

    public GameObject ghostAnchor;

    private void Start()
    {
        StartCoroutine(resetPosition());
    }

    void Update()
    {
        if (transform.position != ghostAnchor.transform.position && !GetComponentInChildren<TapToPlaceGhost>().move
             && !GetComponentInChildren<GhostZone>().placing)
        {
            StartCoroutine(resetPosition());
        }
    }

    public IEnumerator resetPosition()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.position = ghostAnchor.gameObject.transform.position;
    }
}
