using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectInfo
{
    public static string[] escapingWord = {"\"", "\n", "\\"};
    public static string[] escapedWord = {"\\\"", "\\n", "\\\\"};

    private string image_;
    private string position_;
    private string message_;
    private Vector3 originPos_;


    public ObjectInfo()
    {
        image_ = "";
        position_ = "";
        message_ = "";
    }

    public ObjectInfo(string image, string position, string message)
    {
        image_ = image;
        position_ = position;
        message_ = message;
    }

    public string GetImage()
    {
        return image_;
    }

    public string GetPosition()
    {
        return position_;
    }

    public Vector3 GetOriginPos(){
        return originPos_;
    }

    public string GetMessage()
    {
        return message_;
    }

    public void SetImage(string image)
    {
        image_ = image;
    }
    
    public void SetPosition(Vector3 position)
    {
        originPos_ = position;
        position_ = "[" + position.x + "," + position.y + "," + position.z + "]";
    }

    public void SetMessage(string message)
    {
        message_ = Escaping.Escape(message);

    }
    
    /// <summary>
    /// Convert to json data
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("\"image_link\": \"{0}\",\"position\": \"{1}\",\"message\": \"{2}\"", image_, position_, message_);
    }
}
