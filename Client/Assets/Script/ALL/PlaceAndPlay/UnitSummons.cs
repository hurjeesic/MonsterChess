﻿using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class UnitSummons : MonoBehaviour
    {
        void Start()
        {
            Transform[] unitBtnTrans = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform slotTrans in unitBtnTrans)
            {
                Button btn = slotTrans.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => SelectSummons(slotTrans));
                }
            }
        }

        void SelectSummons(Transform slotTrans)
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                int tempNum = int.Parse(slotTrans.name);
                string id = Data.Instance.units[tempNum];

                //소환부분 UI바 구현해봤어요
                UIBar tm = new UIBar();
                tm.UiBarID(id);

                if (Data.Instance.bSummons == false)
                {
                    Data.Instance.bSummons = true;
                    Data.Instance.summonId = id;
                    Debug.Log(Data.Instance.summonId + " 선택");
                }
                else
                {
                    if (Data.Instance.summonId == id) Data.Instance.bSummons = false;
                    else
                    {
                        Data.Instance.bSummons = true;
                        Data.Instance.summonId = id;
                    }
                }
            }
            else if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Play)
            {
                int tempNum = int.Parse(slotTrans.name);
                string id = Data.Instance.units[tempNum];
                int cost = int.Parse(Data.Instance.FindStateOfMonster(id).Substring(6, 1));
                if (Data.Instance.bSummons == false && Data.Instance.time <= 30 && Data.Instance.mana >= cost)
                {
                    Data.Instance.bSummons = true;
                    Data.Instance.summonId = id;
                    Debug.Log(Data.Instance.summonId + " 선택");
                }
                else if (Data.Instance.bSummons == true && Data.Instance.time <= 30 && Data.Instance.mana >= cost)
                {
                    if (Data.Instance.summonId == id) Data.Instance.bSummons = false;
                    else
                    {
                        Data.Instance.bSummons = true;
                        Data.Instance.summonId = id;
                    }
                }
                else
                {
                    Debug.Log("소환을 할 수 없습니다.");
                }

            }
        }
    }
}
