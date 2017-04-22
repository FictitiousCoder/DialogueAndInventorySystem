# Dialogue & Inventory System
Unity Assets one might find handy for creating a text-focused game, but may be suited any application or game in need of the contained assets in general.

Both the dialogue system and the inventory system is heavily inspired by, but by no means meant to replicate, those seen in the Ace Attorney series.

## Dialogue System
The scrolling text, potentially accompanied by a continuous sound effect, makes the simple text displayed far more engaging compared to if all the text was simply instantaneously displayed on the screen. Furthermore, the scrolling text, with pauses to the scrolling at given punctuation marks, adds a sense of rhythm to the text. It makes it "sound" more like the text is actually being spoken at the time of scrolling, which is exactly what we want for our dialogue. The pause should also be dependent on the punctuation mark: a full stop for example, should cause a longer pause than a comma. Text progression is controlled by the user in the sense that they push a key of your chocie to progress the next piece of text, and also allows the user to push and hold a key to speed up the scrolling effect.

### How it works:
For use, it is recommended you utilize the UI-prefab or at least the DialogueCanvas prefab to set up the dialogue window.
The **_DialogueCanvas_** contains the DialogueScript, which is what handles the actual display and scrolling of the text, as well as the user's interaction with it. It imports dialogue to be printed to the screen, does so according to the settings given in the inspector. It also prints potential answers (if the current dialogue provides the user with any options), accepts the user's input, and responds as you set it to in the inspector.

The actual dialogue is contained within a **_DialogueContainer_** script. Simply place the script onto a game object and type in the dialogue you wish. To keep all your dialogue tidy and redeable, I'd advise to set up a parent game object containing all the objects containing your DialogueContainers. Furthermore, it would be wise to name your containers according to what piece of dialogue they belong to, and if the amoutns of containers grow large, you'd do well to more categorizing parent objects inside your outer parent.

The DialogueContainer object decides the behaviour of the dialogue on a per-dialogue basis, and allows you to set:
* The dialogue text and amount of lines.
* Whether there should be answers, the amount of answers, and the effect of giving any one of those answers.
* The next piece of dialogue to load(this is another object with a DialogueContainer. based on the answers, if an answer should trigger more dialogue.
* What happens when the dialogue is finished: finish the dialogue, load a new piece of dialogue, or load an entirely new scene.
* Whether the dialogue should progress on its own, effectively disabling the user's input.
* Whether to disable other user control's (such as player movement) while the dialogue is active.
* What items or profiles—if any—should be recieved upon finishing the dialogue (this is for use in tandem with the inventory system).

**_DialogueTrigger_** is a script you can utilize to trigger a piece of dialogue to begin printing to the screen and activate the dialogue UI. This is first and foremost for use with some sort of user-controlled game object within a scene. It can be used in both 2D and 3D spaces. Simply place the script onto a game object together with a corresponding trigger-collider. Upon the correct game obejct(by default this is whatever object bears the tag of 'Player) entering the collider, the dialogue is triggered either automatically or through player input, depending on what trigger type you choose in the inspector. You may also choose whether you want the dialogue to be repeatable, or whether you want it to only be triggered once. Lastly, in the object bearing the trigger, you need to reference the DialogueContainer you want it to trigger.


## Inventory System
The inventory system is modular in the sense that it functions independently of everything aside from the code used to store the data needed to create and add objects to the inventory. It allows the user to recieve elements that are added into their virtual inventory, allowing them to view it and browse through all previously acquired elements via the interface.  As is, the inventory is split into two sections: items and profiles. This, however, is easy to alter. Removing a line of code or two will restrict the menu to one single section, and changes may also be made to further section the invnetory into different parts as desired. The interface can be controlled both by keyboard and mouse-clicks.

### How it works:
The general idea and set-up for the inventory system is not too unlike that of the dialogue system.
The **_InvnetoryMenu_** script contains most of the code that makes the actual interface function, and allows the user to interact with it. It creates new objects, adds them to them to the list, and removes objects as needed. As it handles both the visuals and the user-input's effect on it, it also allows for all key-bindings to be selected to your liking through the inspector.

An instance of the **_InvnetoryElement_** represents a single element within the invnetory, and contains all information pertaining to that specific element (name, description, image, etc.). In my case I've split my invnetory and elements into items and profiles, which are represented by objects of the child classes **_InvnetoryItem_** and **_InvnetoryProfile_** respectively.

The **_InvnetoryItemPickUp_** and **_InvnetoryProfilePickUp_** scripts are the equivalent of a container for inventory elements that are not yet in the user's inventory. Place it onto a game object and use the inspector to enter the values needed for the element. If the user claims the element, the info you enter will be used to create the object representing that element run-time, and add it to the inventory.
