using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class answerButton : MonoBehaviour {

    public KeyCode key;
    public int answerNumber;
    public DialogueScript dialogueScript;

    public Color defaultColor;
    public Color highlightColor;

    private Text answerText;

	// Use this for initialization
	void Start () {
        answerText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(key))
        {
            answerText.color = highlightColor;
        }

        if (Input.GetKeyUp(key))
        {
            dialogueScript.GiveAnswer(answerNumber);
            answerText.color = defaultColor;
        }
	}
}
