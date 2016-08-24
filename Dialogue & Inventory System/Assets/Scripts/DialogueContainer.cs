using UnityEngine;
using System.Collections;

public class DialogueContainer : MonoBehaviour {

    public DialogueScript dialogueScript;
    public float dialogueSpeed;

    public string speakerName; // Name-box remains hidden if no name is entered
    public string[] dialogue;
    public string[] answers;
    public DialogueContainer[] answerDialogue; // The dialogue to activate based on the given answer. Answer[i] gives dialogue[i]
    public EndType endType;
    public bool autoScroll;
    public bool freezePlayer;

    // If added, these items/profiles is given upon finishing the dialogue
    public InventoryItemPickUp[] recieveItems;
    public InventoryProfilePickUp[] recieveProfiles;

    public void LoadDialogue()
    {
        dialogueScript.enabled = true;
        dialogueScript.ImportDialogueText(dialogue, answers, speakerName, answerDialogue, endType, autoScroll, freezePlayer, recieveItems, recieveProfiles);
    }
}
