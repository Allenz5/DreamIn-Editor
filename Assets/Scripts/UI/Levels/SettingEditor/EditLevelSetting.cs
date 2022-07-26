using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EditLevelSetting : MonoBehaviour
{
    //UI
    public GameObject LevelsUI;
    public GameObject LevelUI;
    public GameObject MapEditorUI;
    public GameObject GameSettingEditorUI;
    public GameObject AnswerPrefab;
    public GameObject AnswersUI;

    public TMP_InputField Title;
    public TMP_InputField Duration;
    public TMP_InputField Summary;
    public TMP_InputField Question;
    public List<GameObject> Answers;

    public GameObject LevelsEditor;

    //Singleton
    public static EditLevelSetting Instance;

    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    #region Buttons
    public void BackButton()
    {
        MapEditorUI.SetActive(true);
        GameSettingEditorUI.SetActive(false);
    }

    public void SaveButton()
    {
        if (Title.text == "")
        {
            Warning.Instance.SetEmptyMessage("GameTitle");
            Warning.Instance.Show();
            return;
        }

        if (Duration.text == "")
        {
            Warning.Instance.SetEmptyMessage("GameTime");
            Warning.Instance.Show();
            return;
        }

        if (Summary.text == "")
        {
            Warning.Instance.SetEmptyMessage("Endmessage");
            Warning.Instance.Show();
            return;
        }

        if (Question.text == "")
        {
            Warning.Instance.SetEmptyMessage("Question");
            Warning.Instance.Show();
            return;
        }

        if (Answers.Count == 0)
        {
            Warning.Instance.SetEmptyMessage("Answer");
            Warning.Instance.Show();
            return;
        }

        LevelsUI.SetActive(true);
        LevelUI.SetActive(false);
        MapEditorUI.SetActive(true);
        GameSettingEditorUI.SetActive(false);

        //Save Data
        EditLevels levels = EditLevels.Instance;
        EditMap map = EditMap.Instance;
        levels.curPanel.GetLevelInfo().SetBackground(map.BackgroundPath);
        levels.curPanel.GetLevelInfo().SetCollideMap(map.ColliderMap);
        levels.curPanel.GetLevelInfo().SetObejcts(map.ObjectInfoList);
        levels.curPanel.GetLevelInfo().SetTitle(Title.text);
        levels.curPanel.GetLevelInfo().SetDuration(int.Parse(Duration.text));
        levels.curPanel.GetLevelInfo().SetSummary(Summary.text);
        levels.curPanel.GetLevelInfo().SetQuestion(Question.text);
        //Set Answers
        List<string> answersStr = new List<string>();
        for (int i = 0; i < Answers.Count; i++)
        {
            answersStr.Add(Answers[i].transform.GetChild(2).GetComponent<TMP_InputField>().text);
        }
        levels.curPanel.GetLevelInfo().SetAnswers(answersStr);

        levels.curPanel.UpdatePanel(Title.text, Duration.text);
        levels.FinishAdding();

        //Clear Data
        map.ClearMap();
        ClearSettings();
    }

    public void AddAnswerButton()
    {
        if (Answers.Count >= 5)
        {
            return;
        }
        Vector3 pos = new Vector3(150, -20 - 30 * Answers.Count, 0);
        GameObject cur = Instantiate(AnswerPrefab, AnswersUI.transform);
        cur.transform.localPosition = pos;

        Answers.Add(cur);
    }

    public void DeleteAnswerButton(GameObject answer)
    {
        Answers.Remove(answer);
        Destroy(answer);
        Reposition();
    }

    private void Reposition()
    {
        for (int i = 0; i < Answers.Count; i++)
        {
            Answers[i].transform.localPosition = new Vector3(150, -20 - 30 * i, 0);
        }
    }

    #endregion

    public void ClearSettings()
    {
        Title.text = "";
        Duration.text = "";
        Summary.text = "";
        Question.text = "";
        
        //Destroy Answers
        for (int i = 0; i < Answers.Count; i++)
        {
            Destroy(Answers[i]);
        }
        Answers.Clear();
    }

    public void FillSettings(string title, int duration, string summary, string question, List<string> answers)
    {
        Title.text = title;
        Duration.text = duration.ToString();
        Summary.text = summary;
        Question.text = question;

        //Create Answers
        for (int i = 0; i < answers.Count; i++)
        {
            AddAnswerButton();
            Answers[i].transform.GetChild(2).GetComponent<TMP_InputField>().text = answers[i];
        }
    }
}
