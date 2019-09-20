using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
namespace MonsterChessClient
{
    public class SommonButton : MonoBehaviour
    {
        // Use this for initialization
        public void OnSommon()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                int TempNum = int.Parse(gameObject.name);
                string ID = Data.Instance.Unit[TempNum];
                if (Data.Instance.SommonOn == false)
                {
                    Data.Instance.SommonOn = true;
                    Data.Instance.SommonID = ID;
                }
                else
                {
                    if (Data.Instance.SommonID == ID) { Data.Instance.SommonOn = false; }
                    else
                    {
                        Data.Instance.SommonOn = true;
                        Data.Instance.SommonID = ID;
                    }
                }
            }
               
        }
    }

}
