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
    public Image identityDisplay;
    [HideInInspector]
    public bool isComplete;
    // Start is called before the first frame update
    // void Start()
    // {
    //     btn.onClick.AddListener(() =>
    //     {
    //         EditCharacters editor = EditCharacters.Instance;
    //         editor.curPanel = this;
    //         editor.SwitchToCharacter();
    //     });
    // }

    public void OnClick(){
        EditCharacters editor = EditCharacters.Instance;
        editor.curPanel = this;
        editor.SwitchToCharacter();
    }

    public void Delete(){
        EditCharacters editor = EditCharacters.Instance;
        editor.DeleteButton(this);
    }



}
