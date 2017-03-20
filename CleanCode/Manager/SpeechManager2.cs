using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SpeechManager2 : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public List<VideoController> videoControllers;
    public List<TapToPlaceGhost> ghostZones;
    public List<VideoHider> videoHiders;
    public List<Hider> hiders;

    private RoomManager roomManager;

    void Start()
    {
        roomManager = GetComponent<RoomManager>();

        keywords.Add("Reset scene", () =>
        {
            SceneManager.LoadScene("Scene002");
            foreach (var ghostZone in ghostZones)
            {
                ghostZone.resetTargetPosition();
            }
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
        });

        keywords.Add("Enter edition mode", () =>
        {
            roomManager.editionMode = true;
            foreach (var videoHider in videoHiders)
            {
                if (videoHider.gameObject.name != "Wendake1")
                {
                    videoHider.proximityStop = 10.0f;
                }
                else
                {
                    videoHider.proximityStop = 2.75f;
                }
            }
            foreach (var hider in hiders)
            {
                hider.show();
            }
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
        });

        keywords.Add("Enter play mode", () =>
        {
            roomManager.editionMode = false;
            foreach (var hider in hiders)
            {
                hider.hide();
            }
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
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
