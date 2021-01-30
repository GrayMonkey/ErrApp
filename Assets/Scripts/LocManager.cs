﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LocManager : MonoBehaviour
{
    public static LocManager instance;
    public List<LocTextData> locText = new List<LocTextData>();
   
    [SerializeField] SystemLanguage gameLang;
    private Translate[] trans;

    private void Awake()
    {
        instance = this;
        GameLang = Application.systemLanguage;
    }

    private void Start()
    {
//        trans = Resources.FindObjectsOfTypeAll<Translate>();
        SetDefaultLang(GameLang);
    }

    public SystemLanguage GameLang
    {
        get
        {
            return gameLang;
        }

        set
        {
            gameLang = value;
            SetDefaultLang(gameLang);
        }
    }

    private void SetDefaultLang(SystemLanguage lang)
    {
        trans = FindObjectsOfType<Translate>();
        string m_lang = lang.ToString();
        string file = "LocText - " + m_lang;
        locText.Clear();

        TextAsset textData = Resources.Load<TextAsset>(file);

        //Remove any hard returns, new lines and double quotes
        //string[] rowData = textData.text.Split(new char[] { '\n' });
        string[] rowData = textData.text.Split(new char[] { '\r' , '\n' },System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < rowData.Length; i++)
        {
            //Split on the first comma
            int firstComma = rowData[i].IndexOf(",");
            string dataKey = rowData[i].Substring(0, firstComma);
            string dataValue = rowData[i].Substring(firstComma + 1);
            dataValue = dataValue.Replace("\\n", "\n");

            //Remove double quotes from beginning and end of string
            if (dataValue.StartsWith("\""))
            {
                dataValue = dataValue.Substring(1, dataValue.Length - 2);
                if(dataValue.Contains("\\n"))
                {
                    Debug.Log(dataValue);
                }
            }

            //Add data to locText
            LocTextData data = new LocTextData();
            data.key = dataKey;
            data.text = dataValue;
            locText.Add(data);
        }

        // Update the activate Text assests with Translate
        foreach (Translate _trans in trans)
        {
            _trans.UpdateString();
        }


    }

    public string GetLocText(string key)
    {
        LocTextData text = locText.Find(x => x.key == key);
        if (text == null)
        {
            return ("Key for text not found!");
        }
        else if (text.text == null)
        {
            return ("Translation not found");
        }
        else
        {
            string returnString = text.text;
            if(returnString.Contains("%%PlayerName"))
                {
                string playerName = PlayerController.instance.activePlayer.playerName;
                returnString = returnString.Replace("%%PlayerName", playerName);
                }
            return returnString;
        }
    }
}
[System.Serializable]
public class Langs
{
    public bool english;
    public bool french;
    public bool german;
    public bool italian;
    public bool spanish;
}