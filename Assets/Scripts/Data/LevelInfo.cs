using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LevelInfo {
    private string title_;
    private int duration_;
    private string end_;
    private string question_;
    private List<string> answers_;

    private string background_ = "";
    private List<ObjectInfo> objects_;
    private bool[,] collideMap_;

    public LevelInfo()
    {
        Debug.Log("Level Created");
        title_ = "";
        duration_ = 1;
        end_ = "";
        background_ = "";
        objects_ = new List<ObjectInfo>();
        collideMap_ = new bool[0,0];
    }

    public void SetTitle(string title)
    {
        title_ = title;
    }

    public string GetTitle()
    {
        return title_;
    }

    public void SetDuration(int duration)
    {
        duration_ = duration;
    }

    public int GetDuration()
    {
        return duration_;
    }

    public void SetSummary(string summary)
    {
        end_ = summary;
    }

    public string GetSummary()
    {
        return end_;
    }

    public void SetQuestion(string question)
    {
        question_ = question;
    }

    public string GetQuestion()
    {
        return question_;
    }

    public void SetAnswers(List<string> answers)
    {
        answers_ = new List<string>(answers);
    }

    public List<string> GetAnswers()
    {
        return answers_;
    }

    public void SetBackground(string background)
    {
        background_ = background;
    }

    public string GetBackground()
    {
        return background_;
    }

    public void SetObejcts(List<ObjectInfo> objects)
    {
        objects_ = new List<ObjectInfo>(objects);
    }

    public List<ObjectInfo> GetObjects()
    {
        return objects_;
    }

    public void SetCollideMap(bool[,] collideMap)
    {
        collideMap_ = (bool[,])collideMap.Clone();
    }

    public bool[,] GetCollideMap()
    {
        return collideMap_;
    }

    public override string ToString()
    {
        String answersStr = "", collideMapStr = "", objectsStr = "";
        for (int i = 0; i < answers_.Count; i++)
        {
            answersStr += "\"" + answers_[i] + "\"" + ",";
        }
        if (answersStr != "")
        {
            answersStr = answersStr.Substring(0, answersStr.Length - 1);
        }


        for (int i = 0; i < collideMap_.GetLength(0); i++)
        {
            for (int j = 0; j < collideMap_.GetLength(1); j++)
            {
                if (collideMap_[i, j])
                {
                    string pos = j + ",";
                    collideMapStr += pos;
                }
            }
            collideMapStr += ";";
        }


        for (int i = 0; i < objects_.Count; i++)
        {
            objectsStr += "{" + objects_[i].ToString() + "},";
        }
        if (objectsStr != "")
        {
            objectsStr = objectsStr.Substring(0, objectsStr.Length - 1);
        }


        String levelStr = string.Format("\"title\": \"{0}\",\"duration\": \"{1}\",\"end\": \"{2}\",\"question\":\"{3}\",\"answers\": [{4}],\"background\": \"{5}\",\"collide_map\": \"{6}\",\"object\": [{7}]", title_, duration_, end_, question_, answersStr, background_,
                collideMapStr, objectsStr);

        return levelStr;
    }
}
