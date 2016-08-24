using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueLog : MonoBehaviour {

    public Text logText;
    public GameObject logBox;
    public KeyCode toggleDialogueLog;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(toggleDialogueLog))
        {
            logBox.SetActive(!logBox.active);
        }
    }

    // Adds text to the dialogue log and increments size
    public void LogText(string text, string speaker)
    {
        if (speaker.Length > 0)
        {
            logText.text += ("<b>" + speaker + "</b>: ");
        }
        logText.text += (text + "\n\n");
        RectTransform log = logText.gameObject.GetComponent<RectTransform>();
        log.sizeDelta = new Vector2(log.sizeDelta.x, log.sizeDelta.y + 50);
    }
}
