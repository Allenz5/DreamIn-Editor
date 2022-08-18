using UnityEngine;
using System;
/// <summary>
/// Storing info of characters
/// </summary>
[Serializable]
public class CharacterInfo
{
    private string name_;
    private string story_;

    public CharacterInfo()
    {
        Debug.Log("Character created");
        name_ = "";
        story_ = "";
    }

    public CharacterInfo(string name, string story)
    {
        name_ = name;
        story_ = story;
    }

    public string GetName()
    {
        return name_;
    }

    public string GetStory()
    {
        return story_;
    }

    public void SetName(string name)
    {
        name_ = Escaping.Escape(name);
        
    }

    public void SetStory(string story)
    {
        story_ = Escaping.Escape(story);
    }
    
    /// <summary>
    /// Create Json data
    /// </summary>
    /// <returns>Json Data</returns>
    public override string ToString()
    {
        return string.Format("\"name\": \"{0}\",\"background\": \"{1}\"", name_, story_);
    }
}
