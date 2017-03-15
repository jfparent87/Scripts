using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSphere : MonoBehaviour {

    void OnSelect()
    {
        SceneManager.LoadScene("Scene002");
    }
}
