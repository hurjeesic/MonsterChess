using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class SommonButton : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    public void OnSommon()
    {
        int temp = int.Parse(gameObject.name.Substring(0));
        DataIndex.SommonID = DataIndex.Unit[temp];
        DataIndex.SommonON = true;
        Debug.Log(DataIndex.SommonID);
    }
    public void SommonOnplacement() {
        if (DataIndex.SommonON == true)
        {
            CheckPan();
            int x = Int32.Parse(gameObject.name.Substring(0, 1));
            int y = Int32.Parse(gameObject.name.Substring(2));
            if (x == 3 && y == 0)
            {
                Debug.Log("히어로 칸 입니다.");
            }
            else
            {
                Texture UnitImage = Resources.Load("Image/Unit/" + DataIndex.SommonID) as Texture;
                gameObject.GetComponent<RawImage>().texture = UnitImage;
                gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                DataIndex.pan[x, y] = DataIndex.SommonID;
                DataIndex.SommonON = false;

            }
            
        }


    }
    
    void CheckPan() {
        for (int i=0; i<7; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (DataIndex.pan[i, j] == DataIndex.SommonID)
                {
                    Debug.Log("실행됐음");
                    GameObject temp = GameObject.Find(""+i+","+j);
                    temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                    DataIndex.pan[i, j] = null;
                }
            }
        }


    }
}
