using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum EndType { finished, newDialouge, newScene };

public class DialogueScript : MonoBehaviour {

    // UI Game Objects that displays the text and plays the audio
	public Text myText;
    public Text nameText;
    public Text logText;
    public Text[] answerText;
	public GameObject textBox;
    public GameObject nameBox;
    public GameObject answerBox;
    public GameObject logBox;
	public AudioSource textSound;
	public AudioClip textBip;
    public DialogueLog dialogueLog;

    // Dialogue settings shared by all dialogue
    public float textSpeed;
    public float textSpeedAccelerator = 2f;
    public bool printAnswers;
    public string playerName;
    public string nextSceneName;
	public GameObject characterCtrl; // For re-enabling character control after diaouge, if needed
	public GameObject[] disable; // For disabling gameobjects upon completion, if needed

    // Dialogue attributes held by each dialogue Game Object
    private List<string> dialogueList = new List<string>();
    private List<string> answerList = new List<string>();
    private string speakerName;
    private DialogueContainer nextDialougeDefault;
    private List<DialogueContainer> nextDialogueList = new List<DialogueContainer>(); // The next dialogue, determined by the answer given
    private bool freezePlayer = false; // Disables player-control during dialogue if true
    private bool autoScroll = false; // Makes dialogue progress on its own and ignores player input
    private InventoryItemPickUp[] recieveItems;
    private InventoryProfilePickUp[] recieveProfiles;
    private EndType endType;

    private float commaDelay = 0.4f;
    private float timer = 0.0f;
    private float regularSpeed; // Stores the standard speed for safekeeping

    private int letterIndex = 0;
    private int dialogueIndex = 0;

    bool autoNextInvoked = false; // Dummy to ensure next() is not invoked twice


	void Start()
    {
        this.enabled = false;
		regularSpeed = textSpeed;

		// Position dialouge-box based on screen size
		RectTransform boxTransform = textBox.GetComponent<RectTransform>();
		boxTransform.anchoredPosition = new Vector2(0, -Screen.height/2 + 90);
	}
	 
	void Update()
    {
        if (autoScroll == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                textSpeed = regularSpeed * textSpeedAccelerator;
            }
            else if (textSpeed != regularSpeed)
            {
                textSpeed = regularSpeed;
            }

            // If dialouge-string has reached its last character, allow the user to progress.
            if (Input.GetKeyDown(KeyCode.E) && dialogueList[dialogueIndex].Length == letterIndex
                && answerBox.active == false && logBox.active == false)
            {
                Next();
            }
        }
        else
        {
            if (dialogueList[dialogueIndex].Length == letterIndex && autoNextInvoked == false)
            {
                Invoke("Next", regularSpeed / 30.0f);
                Invoke("AutoNextReset", regularSpeed / 30.0f);
                autoNextInvoked = true;
            }
        }
	}
	
	void FixedUpdate()
    {
        if (dialogueList.Count > 0)
        {
            // If there are still letters left to print, keep going
            if (letterIndex < dialogueList[dialogueIndex].Length)
            {
                // If set time has passed, add another character
                if (timer > 1 / textSpeed)
                {

                    if (dialogueList[dialogueIndex].Substring(letterIndex, 1) == "," ||
                        dialogueList[dialogueIndex].Substring(letterIndex, 1) == "—")
                    {
                        timer = 0.0f - (commaDelay);
                    }
                    else if (dialogueList[dialogueIndex].Substring(letterIndex, 1) == "." ||
                             dialogueList[dialogueIndex].Substring(letterIndex, 1) == "!" || dialogueList[dialogueIndex].Substring(letterIndex, 1) == "?")
                    {
                        timer = 0.0f - (commaDelay * 2);
                    }
                    else
                    {
                        timer = 0.0f;
                    }
                    // Increment
                    myText.text += dialogueList[dialogueIndex].Substring(letterIndex, 1);
                    letterIndex++;
                    textSound.PlayOneShot(textBip, 0.6F);

                }
                timer += Time.deltaTime;
            }
            // Else, if the dialogue has reached its end and there are answers to give, load and display them
            else if (letterIndex == dialogueList[dialogueIndex].Length && dialogueIndex == dialogueList.Count - 1)
            {
                if (answerList.Count > 0)
                {
                    for (int i = 0; i < answerList.Count; i++)
                    {
                        answerText[i].text = "";
                        answerText[i].text += answerList[i];
                    }

                    answerBox.SetActive(true);
                }
            }
        }
	}

    // Progresses to the next string in the current dialogue
	void Next()
    {
        if (dialogueLog != null)
        {
            dialogueLog.LogText(dialogueList[dialogueIndex], speakerName);
        }

        if (dialogueIndex < dialogueList.Count -1)
		{
			myText.text = "";
			dialogueIndex++;
			timer = 0.0f;
			letterIndex = 0;
		}
		else 
		{
			Finish();
		}
	}

    // Ends the current dialogue
	void Finish()
	{
        if (endType == EndType.finished)
		{
            if (freezePlayer == true)
            {
                characterCtrl.SetActive(true);
            }

            myText.text = "";
			dialogueIndex = 0;
			timer = 0.0f;
			letterIndex = 0;
            textBox.SetActive(false);
            this.enabled = false;
		}
		else if (endType == EndType.newDialouge)
		{
            NextDialogue(nextDialougeDefault);
        }
		else if (endType == EndType.newScene)
		{
			SceneFadeInOut.Instance.StartFade(nextSceneName);
		}

		if (disable != null)
		{
			for (int i = 0; i < disable.Length; i++)
			{
				disable[i].SetActive(false);
			}
		}

        if (recieveItems.Length > 0)
        {
            for(int i = 0; i < recieveItems.Length - 1; i++)
            {
                recieveItems[i].AddItemToInventory();
            }
        }
        if (recieveProfiles.Length > 0)
        {
            for (int i = 0; i < recieveProfiles.Length - 1; i++)
            {
                recieveProfiles[i].AddprofileToInventory();
            }
        }
	}


    public void GiveAnswer(int answer)
    {
        answer--; // Decrement to fit with zero-based index
        answerBox.SetActive(false);

        // If printing answer, do a trick and add the answer to the current dialogueList
        if (printAnswers == true)
        {
            dialogueList.Add(answerList[answer]);
            answerList.Clear();
            endType = EndType.newDialouge;
            nextDialougeDefault = nextDialogueList[answer];
            Next();

            if (speakerName.Length > 0)
            {
                speakerName = playerName;
                nameText.text = speakerName;
                nameBox.SetActive(true);
            }
            else
            {
                nameBox.SetActive(false);
            }
        }
        else if (nextDialogueList[answer] != null)
        {
            if (dialogueLog != null)
            {
                dialogueLog.LogText(dialogueList[dialogueIndex], speakerName);
                dialogueLog.LogText(answerList[answer], playerName);
            }
            NextDialogue(nextDialogueList[answer]);
        }
    }

    void NextDialogue(DialogueContainer nextDialogue)
    {
        myText.text = "";
        dialogueIndex = 0;
        timer = 0.0f;
        letterIndex = 0;

        if (freezePlayer == true)
        {
            characterCtrl.SetActive(true);
        }

        nextDialogue.LoadDialogue();
    }
    // Replace old dialogue text with a new set of strings and properties
    public void ImportDialogueText(string[] dialogue, string[] answers, string speakerName, DialogueContainer[] nextDialogues,
        EndType endType, bool autoScroll, bool freezePlayer, InventoryItemPickUp[] recieveItems, InventoryProfilePickUp[] recieveProfiles)
    {
        myText.text = "";
        textBox.SetActive(true);
        dialogueList.Clear();
        answerList.Clear();
        nextDialogueList.Clear();

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueList.Add(dialogue[i]);
        }

        for (int i = 0; i < answers.Length; i++)
        {
            answerList.Add(answers[i]);
        }

        for (int i = 0; i < nextDialogues.Length; i++)
        {
            if (nextDialogues[i] != null)
            {
                nextDialogueList.Insert(i, nextDialogues[i]);
            }     
        }

        this.speakerName = speakerName;
        this.endType = endType;
        this.autoScroll = autoScroll;
        this.freezePlayer = freezePlayer;
        this.recieveItems = recieveItems;
        this.recieveProfiles = recieveProfiles;

        if (speakerName.Length > 0)
        {
            nameText.text = speakerName;
            nameBox.SetActive(true);
        }
        else
        {
            nameBox.SetActive(false);
        }

        if (this.freezePlayer == true)
        {
            characterCtrl.SetActive(false);
        }

    }

    // Ensures Next() is only called once at a time when in autoProgress-mode
    void AutoNextReset()
    {
        autoNextInvoked = false;
    }

}
