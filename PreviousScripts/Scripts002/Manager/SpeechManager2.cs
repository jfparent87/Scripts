using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SpeechManager2 : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public Instanciator intanciator;
    public List<Hider> buttons;

    void Start()
    {
        keywords.Add("Reset world", () =>
        {
            SceneManager.LoadScene("Scene002");
        });

        keywords.Add("Show horse", () =>
        {
            intanciator.show();
        });

        keywords.Add("Show buttons", () =>
        {
        for (int i = 0; i < buttons.Count; i ++ ){
                buttons[i].show();
        }
        });

        keywords.Add("Hide buttons", () =>
        {
            for (int i = 0; i < buttons.Count(); i++)
            {
                buttons[i].hide();
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
