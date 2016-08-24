using UnityEngine;
using System.Collections;

public enum TriggerType { Automatic, ButtonPress }

public class DialougeTrigger : MonoBehaviour {

    
    public TriggerType triggerType;
    public bool repeat;

    public DialogueContainer dialogueContent;
    public DialogueScript dialouge;

	private bool inRange = false;
    private bool activated = true; // Keeps dialogue from restarting mid-dialogue

	
	// Update is called once per frame
	void Update () {

        if (inRange == true && activated == true)
        {
            if (triggerType == TriggerType.ButtonPress && Input.GetKeyDown(KeyCode.R))
            {
                dialogueContent.LoadDialogue();
                activated = false;

                // Unless trigger can be activated repeatedly, disble it
                if (repeat == false)
                {
                    this.enabled = false;
                }
            }
            else if (triggerType == TriggerType.Automatic)
            {
                dialogueContent.LoadDialogue();
                activated = false;

                if (repeat == false)
                {
                    this.enabled = false;
                }
            }


        }

	}

	void FixedUpdate ()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			inRange = false;
		}
	}
}
