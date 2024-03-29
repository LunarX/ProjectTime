﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

//Top of the script
#pragma warning disable 0649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Text;
using System.IO;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    private string sBpm = "";    // Pour stocker les BPM
    private string level;
    private string bulletTime;
    private List<int> machin = new List<int>() { 0 };

    public List<int> valueList = new List<int>() { 0 };

    void Start()
    {
        GetBPM();
        Graph();

    }

    private void Graph() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        //List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 15, 15, 15 };
        ShowGraph(valueList, (int _i) => "" + (_i+1), (float _f) => " " + Mathf.RoundToInt(_f));
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null) {
        if (getAxisLabelX == null) {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null) {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 760f/valueList.Count;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);
            
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -3f);
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++) {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
            
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    public void GetBPM()
    {
        sBpm = PlayerPrefs.GetString("BPM", sBpm);
        if (sBpm != "X")
        {
            foreach (string c in sBpm.Split(' '))
            {
                if (c.Length > 0)
                    valueList.Add(Int16.Parse(c));
            } 
        }

        Save(sBpm);

    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
        return Application.dataPath + "/Saved_data.txt";
    }

    void Save(String sb)
    {
        string filePath = getPath();
        string level = "";

        // Mettre sous forme de texte
        int intLevel = PlayerPrefs.GetInt("Difficulty");
        if (intLevel == 0)
            level = "easy";
        else if (intLevel == 1)
            level = "medium";
        else if (intLevel == 1)
            level = "hard";

        string sBT = PlayerPrefs.GetString("BulletTime");
        string sPattern = PlayerPrefs.GetString("Pattern");

        int hexa = PlayerPrefs.GetInt("Zombie");

        string hexaS = "HexaOff";
        if (hexa == 1)
            hexaS = "HexaOn";

        int musikS = PlayerPrefs.GetInt("Music");
        string musik = "MusicOff";
        if (musikS == 1)
            musik = "MusicOn";

        int bgS = PlayerPrefs.GetInt("BackGround");
        string bg = "BGoff";
        if (bgS == 1)
            bg = "BGon";
    
        string bpmTime = PlayerPrefs.GetString("BPM-Time");

        string bpmOn = PlayerPrefs.GetString("BPM-On");



        //StreamWriter outStream = System.IO.File.Open(filePath);
        using (StreamWriter outStream = File.AppendText("log.txt"))
        {
            outStream.WriteLine("");
            outStream.WriteLine("BPM        : " + sb);
            outStream.WriteLine("BPM-Detect : " + bpmOn);
            outStream.WriteLine("BPMTime    : " + bpmTime);
            outStream.WriteLine("Info-Level : " + level + "," + hexaS + "," + bg);
            outStream.WriteLine(sBT);
            outStream.WriteLine(sPattern);
        }
            
        //outStream.Close();
    }

}

