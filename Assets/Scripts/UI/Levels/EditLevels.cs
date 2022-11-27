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
    private int COLLIDER_SIZE = 32;

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
        if(EditMap.Instance != null)
        {
            EditMap.Instance.ResetMap();
        }
        if(EditLevelSetting.Instance != null)
        {
            EditLevelSetting.Instance.ClearSettings();
        }
    }

    public void AddLevel(GameMap m)
    {
        GameObject cur = Instantiate(LevelTag, LevelsUI.transform);
        LevelPanel currentPanel = cur.GetComponent(typeof(LevelPanel)) as LevelPanel;
        currentPanel.GetLevelInfo().SetBackground(m.background);
        currentPanel.GetLevelInfo().SetTitle(m.title);
        currentPanel.GetLevelInfo().SetDuration(int.Parse(m.duration));
        currentPanel.GetLevelInfo().SetSummary(m.end);
        currentPanel.GetLevelInfo().SetQuestion(m.question);

        Debug.Log(m.collide_map);
        Sprite bg = Resources.Load<Sprite>(m.background);
        bool[,] ColliderMap = new bool[Mathf.CeilToInt(bg.texture.height / COLLIDER_SIZE), Mathf.CeilToInt(bg.texture.width / COLLIDER_SIZE)];
        string[] rows = m.collide_map.Split(';');
        for (int i = 0; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split(',');
            for (int j = 0; j < cols.Length - 1; j++)
            {
                int idx = int.Parse(cols[j]);
                ColliderMap[i, idx] = true;
                Debug.Log(idx);
                Debug.Log(i);
            }
        }
        currentPanel.GetLevelInfo().SetCollideMap(ColliderMap);

        List<ObjectInfo> objs = new List<ObjectInfo>();
        for (int i = 0; i < m.map_object.Count; i++)
        {
            ObjectInfo obj = new ObjectInfo();
            obj.SetImage(m.map_object[i].image_link);
            obj.SetMessage(m.map_object[i].message);
            obj.SetPosition(m.map_object[i].GetPosition());
            objs.Add(obj);
        }
        currentPanel.GetLevelInfo().SetObejcts(objs);

        List<string> answersStr = new List<string>(m.answers);
        currentPanel.GetLevelInfo().SetAnswers(answersStr);

        currentPanel.UpdatePanel(m.title, m.duration);
        LevelPanels.Add(currentPanel);
        RePosition();
        Debug.Log(currentPanel.GetLevelInfo().ToString());
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
            Debug.Log("destroy");
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

    public void ClearLevels()
    {
        while (LevelPanels.Count > 0)
        {
            DeleteButton(LevelPanels[0]);
        }
    }
}
