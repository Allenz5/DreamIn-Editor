using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CharacterEditor : MonoBehaviour
{
    public IdentityEditor identityEditor;
    public StoryEditor storyEditor;

    public void onActive(CharacterInfo info){
        /*
        identityEditor.gameObject.SetActive(true);
        storyEditor.gameObject.SetActive(false);
        CharacterInfo.IdentityType identity = info.GetIdentity();
        if (identity == CharacterInfo.IdentityType.Detective)
        {
            identityEditor.btnGroup.Select(identityEditor.detectiveButton.gameObject);
            identityEditor.SelectIdentity("Detective");
        } else if(identity == CharacterInfo.IdentityType.Suspect)
        {
            identityEditor.btnGroup.Select(identityEditor.suspectButton.gameObject);
            identityEditor.SelectIdentity("Suspect");
        }else
        {
            identityEditor.btnGroup.Select(identityEditor.murdererButton.gameObject);
            identityEditor.SelectIdentity("Murderer");
        }
        storyEditor.name.text = info.GetName();
        storyEditor.story.text = info.GetStory();
        */
    }
}
