using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using EditorLogics;
using TMPro;
/// <summary>
/// Character(s) Editor
/// </summary>
public class EditCharacters : MonoBehaviour
{

    //UI
    public GameObject CharactersUI;
    public GameObject CharacterUI;
    public GameObject LevelsUI;
    public GameObject CharacterTag;
    public GameObject Add;

    //Data
    public List<CharacterPanel> CharacterPanels;
    [HideInInspector]
    public CharacterPanel curPanel;

    //Singleton
    public static EditCharacters Instance;

    //Constant
    private int maxCharacters = 6;

    void Start()
    {
        CharacterPanels = new List<CharacterPanel>();
        Vector3 pos = new Vector3(-300, 0, 0);
        Add.transform.localPosition = pos;
    }

    void Awake()
    {
        if(Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    /// <summary>
    /// Move to Map Editor
    /// </summary>
    public void NextButton()
    {
        CharactersUI.SetActive(false);
        CharacterUI.SetActive(false);
        LevelsUI.SetActive(true);
    }

    public void SaveData()
    {
        List<CharacterInfo> CharacterInfos = new List<CharacterInfo>();
        for (int i = 0; i < CharacterPanels.Count; i++)
        {
            CharacterInfos.Add(CharacterPanels[i].info);
        }
        EditorData.Instance.SetCharacterInfoList(CharacterInfos);
    }

    /// <summary>
    /// Create a new character
    /// </summary>
    public void AddButton()
    {
        int idx = CharacterPanels.Count;
        Vector3 pos = new Vector3(-300, 0, 0);
        pos.x += 120 * idx;
        GameObject cur = Instantiate(CharacterTag, CharactersUI.transform);
        cur.transform.localPosition = pos;
        Vector3 addPos = new Vector3(-300 + 120 * (idx + 1), 0, 0);
        Add.transform.localPosition = addPos;
        curPanel = cur.GetComponent(typeof(CharacterPanel)) as CharacterPanel;
        CharacterPanels.Add(curPanel);
        if(CharacterPanels.Count >= maxCharacters)
        {
            Add.SetActive(false);
        }
        SwitchToCharacter();
        StoryEditor storyEditor = StoryEditor.Instance;
        storyEditor.name.text = "";
        storyEditor.story.text = "";
    }

    public void AddCharacter(GameCharacter c)
    {
        //Calculate the Position
        int idx = CharacterPanels.Count;
        Vector3 pos = new Vector3(-300, 0, 0);
        pos.x += 120 * idx;

        //Create Character Panel
        GameObject cur = Instantiate(CharacterTag, CharactersUI.transform);
        cur.transform.localPosition = pos;
        Vector3 addPos = new Vector3(-300 + 120 * (idx + 1), 0, 0);
        Add.transform.localPosition = addPos;
        curPanel = cur.GetComponent(typeof(CharacterPanel)) as CharacterPanel;
        CharacterPanels.Add(curPanel);
        if (CharacterPanels.Count >= maxCharacters)
        {
            Add.SetActive(false);
        }

        curPanel.info.SetName(c.name);
        curPanel.info.SetStory(c.background);
        curPanel.isComplete = true;
        curPanel.name.text = c.name;
    }

    /// <summary>
    /// Delete a Character
    /// </summary>
    /// <param name="panel"></param>
    public void DeleteButton(CharacterPanel panel){
        CharacterPanels.Remove(panel);
        Destroy(panel.gameObject);
        RePosition();
    }

    /// <summary>
    /// Delete a Character
    /// </summary>
    public void DeleteCurCharacter(){
        if(curPanel == null){
            return;
        }
        DeleteButton(curPanel);
    }

    /// <summary>
    /// Reposition Character Panels
    /// </summary>
    public void RePosition()
    {
        for(int i =0; i < CharacterPanels.Count; i++)
        {
            Vector3 pos = new Vector3(-300 + 120 * i, 0, 0);
            CharacterPanels[i].transform.localPosition = pos;
        }
        Vector3 addPos = new Vector3(-300 + 120 * CharacterPanels.Count, 0, 0);
        Add.transform.localPosition = addPos;
        if(CharacterPanels.Count < maxCharacters)
        {
            Add.SetActive(true);
        } 
    }

    /// <summary>
    /// Move to Character Editor
    /// </summary>
    public void SwitchToCharacter()
    {
        CharacterEditor characterEditor = CharacterUI.GetComponent<CharacterEditor>();
        CharactersUI.SetActive(false);
        CharacterUI.SetActive(true);
    }

    /// <summary>
    /// Move to Character(s) Editor
    /// </summary>
    public void SwitchToCharacters()
    {
        CharactersUI.SetActive(true);
        CharacterUI.SetActive(false);
    }

    /// <summary>
    /// Delete all Characters
    /// </summary>
    public void ClearCharacters(){
        while(CharacterPanels.Count > 0 ){
            DeleteButton(CharacterPanels[0]);
        }
    }
}
