using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class SommonButton : MonoBehaviour
{
    static Data DataIndex = Data.Instance;
    // Use this for initialization
    public void OnSommon()
    {
        int TempNum = int.Parse(gameObject.name);
        string ID = DataIndex.Unit[TempNum];
        if (DataIndex.SommonOn == false)
        {
            DataIndex.SommonOn = true;
            DataIndex.SommonID = ID;
        }
        else
        {
            if (DataIndex.SommonID == ID) { DataIndex.SommonOn = false; }
            else
            {
                DataIndex.SommonOn = true;
                DataIndex.SommonID = ID;
            }
        }
    }
}
   