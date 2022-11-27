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
        UnityWebRequest request = new UnityWebRequest(url, "POST");//method���䷽ʽ��Ĭ��ΪGet��

        request.uploadHandler = new UploadHandlerRaw(postBytes);//ʵ�����ϴ�������
        request.downloadHandler = new DownloadHandlerBuffer();//ʵ�������ش�����

        request.SendWebRequest();//��������
#if UNITY_EDITOR
            while (!request.isDone)
            {
                //Debug.Log("wait");
            }
            Debug.Log("Status Code: " + request.responseCode);//��÷���ֵ
            if (request.responseCode == 200)//�����Ƿ�ɹ�
            {
                string text = request.downloadHandler.text;//��ӡ���ֵ
                Debug.Log(text);
       
            }
            else
            {
                Debug.Log("postʧ����");
                Debug.Log(request.error);
                Debug.Log(request.responseCode);
            }
#endif

    }
}
