using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using LitJson;
using TMPro;
//using UnityEditor.U2D.Animation;

namespace EditorLogics
{
    public class EditorData : MonoBehaviour
    {
        public static EditorData Instance;
        //Basic info of game
        public string user_id = "";
        public string game_id = "";
        public int status;
        public int game_type = 1;
        public string name;
        public string summary;

        //Characters of this game
        public List<CharacterInfo> CharacterInfoList;

        //Levels of this game
        public List<LevelInfo> LevelInfoList;

        //Saved Data
        GameData gameData;

        void Awake()
        {
            if (Instance == null || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        public override string ToString()
        {
            //Characters Info
            String characterInfoStr = "";
             for (int i = 0; i < CharacterInfoList.Count; i++)
            {
                characterInfoStr += "{" + CharacterInfoList[i].ToString() + "},";
            }
            if (characterInfoStr != "") {
                characterInfoStr = characterInfoStr.Substring(0, characterInfoStr.Length - 1);
            }

            //Levels Info
            String levelInfoStr = "";
            for (int i = 0; i < LevelInfoList.Count; i++)
            {
                levelInfoStr += "{" + LevelInfoList[i].ToString() + "},";
            }
            if (levelInfoStr != "")
            {
                levelInfoStr = levelInfoStr.Substring(0, levelInfoStr.Length - 1);
            }

            //num of player
            string numOfPlayer = CharacterInfoList.Count.ToString();

            //duration of whole game
            int duration = 0;
            for (int i = 0; i < LevelInfoList.Count; i++)
            {
                duration += LevelInfoList[i].GetDuration();
            }

            //cover url
            string coverOfGame = LevelInfoList[0].GetBackground();
            coverOfGame = "https://raw.githubusercontent.com/hanxuan5/DreamIn-Assets/master/" + coverOfGame + ".png";
            coverOfGame = coverOfGame.Replace(" ", "%20");

            String gameDataStr = "{" + string.Format("\"name\": \"{0}\",\"players_num\": \"{1}\",\"map\": [{2}],\"character\": [{3}]", name, numOfPlayer, levelInfoStr,
                characterInfoStr) + "}";
            String editorDataStr = "{" + string.Format("\"user_id\": \"{0}\", \"game_id\": \"{1}\", \"name\": \"{2}\", \"players_num\": \"{3}\", \"status\": \"{4}\", \"game_time\": \"{5}\", \"game_type\": \"{6}\", \"cover\": \"{7}\", \"summary\": \"{8}\", \"infos\": {9}", user_id, game_id, name, numOfPlayer, status, duration, game_type, coverOfGame, summary, gameDataStr) + "}";
            return editorDataStr;
        }

        #region Fetch & Write Saved Data
        IEnumerator GetGameData(string ID)
        {
            string url = "https://api.dreamin.land/get_game_doc/";
            UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

            Encoding encoding = Encoding.UTF8;
            byte[] buffer = encoding.GetBytes("{\"id\":" + ID + "}");
            webRequest.uploadHandler = new UploadHandlerRaw(buffer);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("get game data succcess!");
            }

            //read and store in gameData
            ReceiveData d = JsonMapper.ToObject<ReceiveData>(webRequest.downloadHandler.text);
            gameData = JsonMapper.ToObject<GameData>(d.game_doc);
            game_id = gameData._id;
        }

        public void FillCharactersData()
        {

        }

        public void FillLevelsData()
        {

        }

        public void FillGameSettingsData()
        {
            
        }
        #endregion

        #region Getter & Setter
        public string GetUserId()
        {
            return user_id;
        }

        public string GetName(){
            return name;
        }

        public void SetUserId(string newId)
        {
            user_id = newId;
        }

        public void SetGameId(string newId)
        {
            game_id = newId;
        }

        public void SetName(string newName){
            name = newName;
        }

        public void SetSummary(string newSummary)
        {
            summary = Escaping.Escape(newSummary);
        }

        public void SetStatus(int newStatus)
        {
            status = newStatus;
        }

        public void SetLevelInfoList(List<LevelInfo> infos)
        {
            LevelInfoList = new List<LevelInfo>(infos);
        }

        public void SetCharacterInfoList(List<CharacterInfo> infos){
            CharacterInfoList = new List<CharacterInfo>(infos);
        }

        #endregion
    }
}