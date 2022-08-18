using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escaping
{
    public static string[] escapingWord = {"\\","\"", "\n"};
    public static string[] escapedWord = {"\\\\", "\\\"", "\\n"};

    public static string Escape(string data){
        for(int i = 0; i < escapingWord.Length; i++){
            data  = data.Replace(escapingWord[i], escapedWord[i]);
        }
        return data;
    }
}
