using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPanel : MonoBehaviour
{
    public void Delete()
    {
        Debug.Log("Delete");
        EditLevelSetting editor = EditLevelSetting.Instance;
        editor.DeleteAnswerButton(this.gameObject);
    }
}
