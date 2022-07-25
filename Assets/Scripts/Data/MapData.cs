using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace EditorLogics
{
    public class MapData
    {
        //Path of backgournd image
        string background = "";
        //Objects
        List<ObjectInfo> objects;
        //Size of collider
        int COLLIDE_MAP_SIZE = 32;
        //Colliders
        bool[,] collideMap;

        public String GetBackground()
        {
            return background;
        }

        public List<ObjectInfo> GetObjects()
        {
            return objects;
        }

        public bool[,] GetCollideMap(){
            return collideMap;
        }

        public int GetCollideMapSize()
        {
            return COLLIDE_MAP_SIZE;
        }

        public void SetCollideMapSize(int size)
        {
            COLLIDE_MAP_SIZE = size;
        }

        public void SetCollideMap(bool[,] newCollideMap){
            collideMap = newCollideMap;
        }
        
        public void SetBackground(String bg)
        {
            background = bg;
        }

        public void SetObejcts(List<ObjectInfo> objectInfos)
        {
            objects = new List<ObjectInfo>(objectInfos);
        }

        /// <summary>
        /// Convert to json data
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String collideMapStr = "",objectsStr = "";
            for (int i=0; i < collideMap.GetLength(0); i++)
            {
                for (int j = 0; j < collideMap.GetLength(1); j++)
                {
                    if (collideMap[i, j])
                    {
                        string pos = j + ",";
                        collideMapStr += pos;
                    }
                }
                collideMapStr += ";";
            }
            for (int i = 0; i < objects.Count; i++)
            {
                objectsStr += "{" + objects[i].ToString() + "},";
            }
            if(objectsStr != ""){
                objectsStr = objectsStr.Substring(0, objectsStr.Length - 1);
            }
            String mapStr = "{" + string.Format("\"background\": \"{0}\",\"collide_map\": \"{1}\",\"map_object\": [{2}]", background,
                collideMapStr, objectsStr) + "}";

            return mapStr;
        }
    }
}