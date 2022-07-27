using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StoryEditor : MonoBehaviour
{
    public TMP_InputField name;
    public TMP_InputField story;

    //Singleton
    public static StoryEditor Instance;

    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void BackButton()
    {
        if (!EditCharacters.Instance.curPanel.isComplete)
        {
            EditCharacters.Instance.DeleteCurCharacter();
        }
        EditCharacters.Instance.SwitchToCharacters();
    }

    public void SaveButton(){
        if(name.text == ""){
            Warning.Instance.SetEmptyMessage("Name");
            Warning.Instance.Show();
            return;
        }
        if(story.text == ""){
            Warning.Instance.SetEmptyMessage("Story");
            Warning.Instance.Show();
            return;
        }
        EditCharacters editor = EditCharacters.Instance;
        editor.curCharacter.SetName(name.text);
        editor.curCharacter.SetStory(story.text);
        
        editor.AddInfo();
        editor.curPanel.isComplete = true;

        editor.curPanel.name.text = name.text;
        editor.SwitchToCharacters();
    }
}
