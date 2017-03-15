using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SpeechManager2 : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public RoomManager roomManager;
    public List<VideoController> videoControllers;

    void Start()
    {
        keywords.Add("Reset world", () =>
        {
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
            SceneManager.LoadScene("Scene002");
        });

        keywords.Add("Enter edition mode", () =>
        {
            roomManager.editionMode = true;
        });

        keywords.Add("Enter play mode", () =>
        {
            roomManager.editionMode = false;
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
