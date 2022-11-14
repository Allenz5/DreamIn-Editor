using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterPanel : MonoBehaviour
{
    public CharacterInfo info;
    public Button btn;
    public TMP_Text name;
    [HideInInspector]
    public bool isComplete;

    public void OnClick(){
        EditCharacters editor = EditCharacters.Instance;
        editor.curPanel = this;
        StoryEditor storyEditor = StoryEditor.Instance;
        storyEditor.name.text = info.GetName();
        storyEditor.story.text = info.GetStory();
        editor.SwitchToCharacter();
    }

    public void Delete(){
        EditCharacters editor = EditCharacters.Instance;
        editor.DeleteButton(this);
    }
}
