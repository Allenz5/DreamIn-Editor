using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
//using UnityEditor.U2D.Animation;

namespace EditorLogics
{
    public class EditorData : MonoBehaviour
    {
        public static EditorData Instance;
        //Name of this game
        public string name;
        public string summary;

        //Characters of this game
        public List<CharacterInfo> CharacterInfoList;

        //Levels of this game
        public List<LevelInfo> LevelInfoList;

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
            string durationOfGame = duration.ToString();

            //cover url
            string coverOfGame = LevelInfoList[0].GetBackground();
            coverOfGame = "https://raw.githubusercontent.com/hanxuan5/DreamIn-Assets/master/" + coverOfGame + ".png";
            coverOfGame = coverOfGame.Replace(" ", "%20");

            String gameDataStr = "{" + string.Format("\"name\": \"{0}\",\"players_num\": \"{1}\",\"map\": [{2}],\"character\": [{3}]", name, numOfPlayer, levelInfoStr,
                characterInfoStr) + "}";
            String editorDataStr = "{" + string.Format("\"name\": \"{0}\", \"summary\": \"{1}\", \"players_num\": \"{2}\", \"duration\": \"{3}\", \"cover\": \"{4}\", \"infos\": {5}", name, summary, numOfPlayer, durationOfGame, coverOfGame, gameDataStr) + "}";
            return editorDataStr;
        }

        void Awake(){
            if(Instance == null || Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        public string GetName(){
            return name;
        }

        public void SetName(string newName){
            name = newName;
        }

        public void SetSummary(string newSummary)
        {
            summary = Escaping.Escape(newSummary);
        }

        public void SetLevelInfoList(List<LevelInfo> infos)
        {
            LevelInfoList = new List<LevelInfo>(infos);
        }

        public void SetCharacterInfoList(List<CharacterInfo> infos){
            CharacterInfoList = new List<CharacterInfo>(infos);
        }
    }
}