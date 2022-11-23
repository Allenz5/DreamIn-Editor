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
        [HideInInspector] public string user_id = "";
        [HideInInspector] public string game_id = "";
        [HideInInspector] public int status;
        [HideInInspector] public int game_type = 1;
        [HideInInspector] public string name;
        [HideInInspector] public string summary;

        //Characters of this game
        [HideInInspector] public List<CharacterInfo> CharacterInfoList;

        //Levels of this game
        [HideInInspector] public List<LevelInfo> LevelInfoList;

        //Saved Data
        [HideInInspector] ReceiveData gameInfo;
        [HideInInspector] GameData gameData;

        //UI
        public GameObject Character;
        public GameObject Levels;
        public GameObject Level;
        public GameObject SettingEditor;
        public GameObject GameSettings;

        void Awake()
        {
            if (Instance == null || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        void Start()
        {
            //Initialize all Instances
            Character.SetActive(false);
            Levels.SetActive(false);
            Level.SetActive(false);
            SettingEditor.SetActive(false);
            GameSettings.SetActive(false);

            if (game_id != "")
            {
                GetGameData(game_id);
            }
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
            String editorDataStr = "{" + string.Format("\"user_id\": \"{0}\", \"game_id\": \"{1}\", \"name\": \"{2}\", \"players_num\": \"{3}\", \"status\": \"{4}\", \"duration\": \"{5}\", \"game_type\": \"{6}\", \"cover\": \"{7}\", \"summary\": \"{8}\", \"infos\": {9}", user_id, game_id, name, numOfPlayer, status, duration, game_type, coverOfGame, summary, gameDataStr) + "}";
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
            gameInfo = JsonMapper.ToObject<ReceiveData>(webRequest.downloadHandler.text);
            gameData = JsonMapper.ToObject<GameData>(gameInfo.game_doc);
            FillData();
        }

        public void FillData()
        {
            //Fill Characters
            EditCharacters editCharacters = EditCharacters.Instance;
            for (int i = 0; i < gameData.character.Count; i++)
            {
                editCharacters.AddCharacter(gameData.character[i]);
            }

            //Fill Levels
            EditLevels editLevels = EditLevels.Instance;
            for (int i = 0; i < gameData.map.Count; i++)
            {
                editLevels.AddLevel(gameData.map[i]);
            }

            //Fill Game Settings
            EditGameSettings editGameSettings = EditGameSettings.Instance;
            editGameSettings.GameTitle.text = gameData.name;
            editGameSettings.Summary.text = gameInfo.summary;

        }

        public void GetGameScriptID(string id)
        {
            game_id = id;
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