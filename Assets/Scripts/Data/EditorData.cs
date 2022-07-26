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

        //Characters of this game
        public List<CharacterInfo> CharacterInfoList;

        //Levels of this game
        public List<LevelInfo> LevelInfoList;

        public override string ToString()
        {
            String characterInfoStr = "";
             for (int i = 0; i < CharacterInfoList.Count; i++)
            {
                characterInfoStr += "{" + CharacterInfoList[i].ToString() + "},";
            }
            if (characterInfoStr != "") {
                characterInfoStr = characterInfoStr.Substring(0, characterInfoStr.Length - 1);
            }

            String levelInfoStr = "";
            for (int i = 0; i < LevelInfoList.Count; i++)
            {
                levelInfoStr += "{" + LevelInfoList[i].ToString() + "},";
            }
            if (levelInfoStr != "")
            {
                levelInfoStr = levelInfoStr.Substring(0, levelInfoStr.Length - 1);
            }

            string numOfPlayer = CharacterInfoList.Count.ToString();
            String gameDataStr = "{" + string.Format("\"name\": \"{0}\",\"players_num\": \"{1}\",\"map\": [{2}],\"character\": [{3}]", name, numOfPlayer, levelInfoStr,
                characterInfoStr) + "}";
            String editorDataStr = "{" + string.Format("\"name\": \"{0}\",\"players_num\": \"{1}\",\"infos\": {2}", name, numOfPlayer, gameDataStr) + "}";
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

        public void SetLevelInfoList(List<LevelInfo> infos)
        {
            LevelInfoList = new List<LevelInfo>(infos);
        }

        public void SetCharacterInfoList(List<CharacterInfo> infos){
            CharacterInfoList = new List<CharacterInfo>(infos);
        }
    }
}