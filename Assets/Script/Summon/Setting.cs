using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class Setting : MonoBehaviour {
   
    static MonsterData DataIndex = MonsterData.Instance;

   
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < 5; i++)
        {
            GameObject[] SommonButton = new GameObject[5];
            SommonButton[i] = GameObject.Find(""+i);
            string temp = "Image/ButtonUnit/" + DataIndex.Unit[i];
            Debug.Log(temp);
            SommonButton[i].GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
            SommonButton[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            DataIndex.SommonON = false;
        }
        GameObject Hero = GameObject.Find("3,0");
        Hero.GetComponent<RawImage>().texture = Resources.Load("Image/Unit/"+DataIndex.Unit[5]) as Texture;
        Hero.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        DataIndex.pan[3, 0] = DataIndex.Unit[5];

    }


}
