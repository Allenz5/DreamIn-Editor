using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace EditorLogics
{
    /// <summary>
    /// Sending data to backend
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Create Http post
        /// </summary>
        /// <param name="jsonDataPost"></param>
        public static void SendJsonByHttpPost(String jsonDataPost)
        {
            //Debug.Log("测试Post");
            String url = "https://api.dreamin.land/info_post/";
            Encoding encoding = Encoding.UTF8;

            byte[] buffer = encoding.GetBytes(jsonDataPost);
            HttpsPost(url, buffer);
        }
        
        /// <summary>
        /// Handle Http post
        /// </summary>
        /// <param name="URL">访问地址</param>
        /// <param name="postBytes">携带数据</param>
        /// <returns></returns>
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
        
        public bool IsBusy=false;//用于检测是否重复发送
    }
}