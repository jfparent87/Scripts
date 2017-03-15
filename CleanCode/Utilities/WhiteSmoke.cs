using UnityEngine;

public class WhiteSmoke : MonoBehaviour {

	void Start () {
        GetComponent<ParticleSystem>().Stop();
    }

}
