using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;


public class PanButton : MonoBehaviour
{
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    public void ButtonDistinction(){
        if (DataIndex.SommonON == true)
        {
            //소환
            int x = int.Parse(gameObject.name.Substring(0, 1));
            int y = int.Parse(gameObject.name.Substring(2));
            if (DataIndex.pan[x, y] == null)
            {
                Texture UnitImage = Resources.Load("Image/Unit/" + DataIndex.SommonID) as Texture;
                gameObject.GetComponent<RawImage>().texture = UnitImage;
                gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                DataIndex.pan[x, y] = DataIndex.SommonID;
                DataIndex.SommonON = false;
            }
        }
        else if (DataIndex.MoveON == true)
        {
            //이동
           
        }
       

    }
}
