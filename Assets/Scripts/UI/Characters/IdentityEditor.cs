using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class IdentityEditor : MonoBehaviour
{
    public Image identityDisplay;
    public TMP_Text identityText;
    public TMP_Text introductionText;
    [HideInInspector]
    //public CharacterInfo.IdentityType curIdentity;
    public StoryEditor storyEditor;

    [HideInInspector]
    public Dictionary<String, String> introductionTable;
    [HideInInspector]
    //public Dictionary<String, CharacterInfo.IdentityType> identityTable;
    //[HideInInspector]
    public Dictionary<String, Sprite> imageTable;
    public ButtonGroup btnGroup;
    public Button detectiveButton;
    public Button suspectButton;
    public Button murdererButton;
    public Sprite detectiveImage;
    public Sprite suspectImage;
    public Sprite murdererImage;


    // Start is called before the first frame update
    void Awake()
    {
        introductionTable = new Dictionary<string, string>();
        introductionTable.Add("Detective", 
        "The only character above suspicion. Lead the deduction to find the murderer. The vote from this character is counted double.");
        introductionTable.Add("Suspect", 
        "Is suspected but not the murderer.  Defend against allegations and follow the detective to find the murderer.");
        introductionTable.Add("Murderer", 
        "The real murderer. Pretend to be innocent. Lie to escape allegations and frame the other suspects.");
        //identityTable = new Dictionary<string, CharacterInfo.IdentityType>();
        //identityTable.Add("Detective", CharacterInfo.IdentityType.Detective);
        //identityTable.Add("Suspect", CharacterInfo.IdentityType.Suspect);
        //identityTable.Add("Murderer", CharacterInfo.IdentityType.Murderer);
        imageTable = new Dictionary<string, Sprite>();
        imageTable.Add("Detective", detectiveImage);
        imageTable.Add("Suspect", suspectImage);
        imageTable.Add("Murderer", murdererImage);
        identityText.text = "";
        introductionText.text = "";

    }


    public void SelectIdentity(String identity){
        //curIdentity = identityTable[identity];
        identityText.text = identity;
        introductionText.text = introductionTable[identity];
        identityDisplay.sprite =  imageTable[identity];
    }

    public void NextButton(){
        //if(curIdentity == null){
        //    return;
        //}
        gameObject.SetActive(false);
        storyEditor.gameObject.SetActive(true);
        //storyEditor.onActive(identityDisplay.sprite);
    }

    public void BackButton(){
        if(!EditCharacters.Instance.curPanel.isComplete){
            EditCharacters.Instance.DeleteCurCharacter();
        }
        EditCharacters.Instance.SwitchToCharacters();
    }
}
