﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DropMultiTemplateMgr : MUIBase<DropMultiTemplate> {
    string TextResource;
    bool flagDataLoad;

    public DropMultiTemplateMgr()
    {
        flagDataLoad = false;
    }

    static public string ReadFile(string FileName)
    {
        TextAsset TextAsset = (TextAsset)Resources.Load(FileName, typeof(TextAsset));
        if (TextAsset == null) return "";

        byte[] bytes = Encoding.UTF8.GetBytes(TextAsset.ToString());
        string sZData = Encoding.UTF8.GetString(bytes);

        //byte[] bytes = TextAsset.bytes;
        //UTF8Encoding pUTF8 = new UTF8Encoding(true);
        //string sZData = pUTF8.GetString(bytes);

        Resources.UnloadAsset(TextAsset);

        return sZData;
    }

    public void OnDataLoad(string _textResource)
    {
        if (TextResource == "" && _textResource == "") return;

        if (TextResource == "")
        {
            TextResource = _textResource;
        }
        if (TextResource != _textResource)
        {
            TextResource = _textResource;
        }
        else
        {
            if (flagDataLoad) return;
        }

        string Data = ReadFile(_textResource);
        if (Data != "")
        {
            string[] list = Data.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int nCnt = 1; nCnt < list.Length; nCnt++)
            {
                string[] listDetail = list[nCnt].Split('\t');
                int Length = (int)(listDetail.Length - 1);
                listDetail[Length] = listDetail[Length].Trim('\r');
                if (listDetail.Length > 0)
                {
                    DropMultiTemplate pTemplate = new DropMultiTemplate(listDetail);
                    base.AddTItem(pTemplate);
                }
            }
        }
        flagDataLoad = true;
    }

    public DropMultiTemplate GetTemplate(int _Index)
    {
        List<DropMultiTemplate> listTemplate = GetTList();
        for (int nCnt = 0; nCnt < listTemplate.Count; ++nCnt)
        {
            if (listTemplate[nCnt].Index != _Index)
                continue;

            return listTemplate[nCnt];
        }
        return null;
    }
}
