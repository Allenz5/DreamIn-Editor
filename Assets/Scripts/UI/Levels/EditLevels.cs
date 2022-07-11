using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditorLogics;

public class EditLevels : MonoBehaviour
{
    //UI
    public GameObject CharactersUI;
    public GameObject LevelsUI;
    public GameObject LevelUI;
    public GameObject GameSettingUI;
    public GameObject LevelTag;
    public GameObject Add;

    //Data
    public List<LevelPanel> LevelPanels = new List<LevelPanel>();
    [HideInInspector]
    public LevelPanel curPanel;

    //Singleton
    public static EditLevels Instance;

    //Constant
    private int maxLevels = 5;

    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void NextButton()
    {
        LevelsUI.SetActive(false);
        GameSettingUI.SetActive(true);
    }

    public void BackButton()
    {
        LevelsUI.SetActive(false);
        CharactersUI.SetActive(true);
    }

    public void SaveData()
    {
        List<LevelInfo> levelInfoList = new List<LevelInfo>();
        for (int i = 0; i < LevelPanels.Count; i++)
        {
            levelInfoList.Add(LevelPanels[i].GetLevelInfo());
        }
        EditorData.Instance.SetLevelInfoList(levelInfoList);
    }

    public void SwitchToLevel()
    {
        LevelsUI.SetActive(false);
        LevelUI.SetActive(true);
    }

    /// <summary>
    /// Create a new character
    /// </summary>
    public void AddButton()
    {
        GameObject cur = Instantiate(LevelTag, LevelsUI.transform);
        curPanel = cur.GetComponent(typeof(LevelPanel)) as LevelPanel;
        SwitchToLevel();
    }

    public void DeleteButton(LevelPanel panel)
    {
        LevelPanels.Remove(panel);
        Destroy(panel.gameObject);
        RePosition();
    }

    public void FinishAdding()
    {
        if (!LevelPanels.Contains(curPanel))
        {
            LevelPanels.Add(curPanel);
            RePosition();
        }
        Debug.Log(curPanel.GetLevelInfo().ToString());
    }

    public void CancelAdding()
    {
        if (!LevelPanels.Contains(curPanel))
        {
            Destroy(curPanel.gameObject);
        }
    }

    private void RePosition()
    {
        for (int i = 0; i < LevelPanels.Count; i++)
        {
            Vector3 pos = new Vector3(-300 + 120 * i, 0, 0);
            LevelPanels[i].transform.localPosition = pos;
        }
        Vector3 addPos = new Vector3(-300 + 120 * LevelPanels.Count, 0, 0);
        Add.transform.localPosition = addPos;
        if (LevelPanels.Count >= maxLevels)
        {
            Add.SetActive(false);
        }
        else
        {
            Add.SetActive(true);
        }
    }
}
