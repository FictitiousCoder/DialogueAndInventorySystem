# Dialogue & Inventory System
Unity Assets one might find handy for creating a text-focused game, but may be suited any application or game in need of the contained assets in general.

Both the dialogue system and the inventory system is heavily inspired by, but by no means meant to replicate, those seen in the Ace Attorney series.

## Dialogue System
The scrolling text, potentially accompanied by a continuous sound effect, makes the simple text displayed far more engaging compared to if all the text was simply instantaneously displayed on the screen. Furthermore, the scrolling text, with pauses to the scrolling at given punctuation marks, adds a sense of rhythm to the text. It makes it "sound" more like the text is actually being spoken at the time of scrolling, which is exactly what we want for our dialogue. The pause should also be dependent on the punctuation mark: a full stop for example, should cause a longer pause than a comma. Text progression is controlled by the user in the sense that they push a key of your chocie to progress the next piece of text, and also allows the user to push and hold a key to speed up the scrolling effect.

###How to use:
For use, it is recommended you utilize the UI-prefab or at least the DialogueCanvas prefab to set up the dialogue window.
The **_DialogueCanvas_** contains the DialogueScript, which is what handles the actual display and scrolling of the text, as well as the user's interaction with it. It imports dialogue to be printed to the screen, does so according to the settings given in the inspector. It also prints potential answers (if the current dialogue provides the user with any options), accepts the user's input, and responds as you set it to in the inspector.

The actual dialogue is contained within a **_DialogueContainer_** script. Simply place the script onto a game object and type in the dialogue you wish. 
