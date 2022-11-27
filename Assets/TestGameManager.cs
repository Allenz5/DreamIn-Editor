using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using TMPro;

public class TestGameManager : MonoBehaviour
{
    public TMP_InputField game_id;
    public void DeleteButton()
    {
        if (game_id.text != "")
        {
            String url = "https://api.dreamin.land/del_game/";
            Encoding encoding = Encoding.UTF8;
            string jsonDataPost = "{" + string.Format("\"id\": {0}", game_id.text) + "}";
            Debug.Log(jsonDataPost);
            byte[] buffer = encoding.GetBytes(jsonDataPost);
            HttpsPost(url, buffer);
        }
    }

    private static void HttpsPost(string url, byte[] postBytes)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");//method传输方式，默认为Get；

        request.uploadHandler = new UploadHandlerRaw(postBytes);//实例化上传缓存器
        request.downloadHandler = new DownloadHandlerBuffer();//实例化下载存贮器

        request.SendWebRequest();//发送请求
#if UNITY_EDITOR
            while (!request.isDone)
            {
                //Debug.Log("wait");
            }
            Debug.Log("Status Code: " + request.responseCode);//获得返回值
            if (request.responseCode == 200)//检验是否成功
            {
                string text = request.downloadHandler.text;//打印获得值
                Debug.Log(text);
       
            }
            else
            {
                Debug.Log("post失败了");
                Debug.Log(request.error);
                Debug.Log(request.responseCode);
            }
#endif

    }
}
