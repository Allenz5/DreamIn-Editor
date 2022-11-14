using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPanel : MonoBehaviour
{
    public LevelInfo info;
    public TMP_Text Title;
    public TMP_Text Duration;

    public void OnClick()
    {
        EditLevels editor = EditLevels.Instance;
        editor.curPanel = this;
        editor.SwitchToLevel();
        
        EditLevelSetting.Instance.FillSettings(info.GetTitle(), info.GetDuration(), info.GetSummary(), info.GetQuestion(), info.GetAnswers());
        EditMap.Instance.FillMap(info.GetBackground(), info.GetObjects(), info.GetCollideMap());
        Debug.Log(editor.curPanel.GetLevelInfo().ToString());
    }

    public void Delete()
    {
        EditLevels editor = EditLevels.Instance;
        editor.DeleteButton(this);
    }

    public LevelInfo GetLevelInfo()
    {
        return info;
    }

    public void UpdatePanel(string title, string duration)
    {
        Title.text = title;
        Duration.text = duration;
    }
}
