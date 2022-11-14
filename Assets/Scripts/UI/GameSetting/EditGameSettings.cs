using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EditorLogics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Net;
using System.Text;

/// <summary>
/// UI logics of game settings editor
/// </summary>
public class EditGameSettings : MonoBehaviour
{
    public GameObject LevelsUI;
    public GameObject GameUI;
    public TMP_InputField GameTitle;
    public TMP_InputField Summary;
    public GameObject FinishPage;
     

    //Singleton
    public static EditGameSettings Instance;

    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    /// <summary>
    /// Move back to map editor
    /// </summary>
    public void BackButton()
    {
        LevelsUI.SetActive(true);
        GameUI.SetActive(false);
    }

    /// <summary>
    /// Create Json data and publish
    /// </summary>
    public void PublishButton()
    {
        if (GameTitle.text == "")
        {
            Warning.Instance.SetEmptyMessage("GameTitle");
            Warning.Instance.Show();
            return;
        }

        if (Summary.text == "")
        {
            Warning.Instance.SetEmptyMessage("Summary");
            Warning.Instance.Show();
            return;
        }

        EditCharacters.Instance.SaveData();
        EditLevels.Instance.SaveData();
        EditorData data = EditorData.Instance;

        if (data.CharacterInfoList.Count == 0)
        {
            Warning.Instance.SetEmptyMessage("Character");
            Warning.Instance.Show();
            return;
        }

        if (data.LevelInfoList.Count == 0)
        {
            Warning.Instance.SetEmptyMessage("Level");
            Warning.Instance.Show();
            return;
        }

        data.SetName(GameTitle.text);
        data.SetSummary(Summary.text);
        data.SetStatus(0);
        string dataJsonStr = data.ToString();
        Debug.Log(dataJsonStr);

        EditorLogics.Network.SendJsonByHttpPost(dataJsonStr);
        GameUI.SetActive(false);
        FinishPage.SetActive(true);
    }


    /// <summary>
    /// Create Json data and save
    /// </summary>
    public void SaveButton()
    {
        //Prepare Data
        EditCharacters.Instance.SaveData();
        EditLevels.Instance.SaveData();
        EditorData data = EditorData.Instance;
        data.SetName(GameTitle.text);
        data.SetSummary(Summary.text);
        data.SetStatus(1);

        //Check
        if (data.GetUserId() == "")
        {
            Warning.Instance.SetEmptyMessage("You need to register first");
            Warning.Instance.Show();
            return;
        }

        //Construct Json file
        string dataJsonStr = data.ToString();
        Debug.Log(dataJsonStr);

        //Send data
        EditorLogics.Network.SendJsonByHttpPost(dataJsonStr);

        //Finish
        GameUI.SetActive(false);
        FinishPage.SetActive(true);
    }

    /// <summary>
    /// Clear everything
    /// </summary>
    public void ClearSettings()
    {
        GameTitle.text = "";
        Summary.text = "";
    }
}
