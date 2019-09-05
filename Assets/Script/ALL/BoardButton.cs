using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
namespace MonsterChessClient
{
    public class BoardButton : MonoBehaviour
    {
        static Data DataIndex = Data.Instance;
        // Use this for initialization
        public void OnBoard()
        {
            if (DataIndex.SommonOn == true)
            {
                //소환
                int cost = int.Parse(FindStateOfMonster(DataIndex.SommonID).Substring(6, 1));
                int x = int.Parse(gameObject.name.Substring(2));
                int y = int.Parse(gameObject.name.Substring(0, 1));
                if (y < 3)
                {
                    if (x == 3 && y == 0)
                    {
                        Debug.Log("히어로 칸 입니다.");
                    }
                    else
                    {
                        if (DataIndex.Mana - cost >= 0)
                        {
                            if (GameObject.Find("SceneManager").GetComponent<SceneManager>().Present == SceneManager.SceneList.Place){CheckPan();}
                            Texture UnitImage = Resources.Load("Image/UnitMy/" + DataIndex.SommonID) as Texture;
                            gameObject.GetComponent<RawImage>().texture = UnitImage;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            DataIndex.pan[y, x] = DataIndex.SommonID;
                            DataIndex.SommonOn = false;
                            DataIndex.Mana -= cost;
                        }
                        else
                        {
                            Debug.Log("마나가부족합니다.");
                            DataIndex.SommonOn = false;
                        }
                    }
                }
                
            }
            else if (DataIndex.ChangeListOn == true)
            {
                //이동
            }


        }

        string FindStateOfMonster(string ID)
        {
            //ID를 통해 몬스턴 전체 데이터를 가져옴
            switch (ID.Substring(0, 1))
            {
                case "0":
                    for (int i = 0; i < 12; i++)
                    {
                        if (DataIndex.StateOfMonster[0, i].Substring(0, 3) == ID)
                        {
                            return DataIndex.StateOfMonster[0, i];
                        }
                    }
                    break;
                case "1":
                    for (int i = 0; i < 6; i++)
                    {
                        if (DataIndex.StateOfMonster[1, i].Substring(0, 3) == ID)
                        {
                            return DataIndex.StateOfMonster[1, i];
                        }
                    }
                    break;
                case "2":
                    for (int i = 0; i < 3; i++)
                    {
                        if (DataIndex.StateOfMonster[2, i].Substring(0, 3) == ID)
                        {
                            return DataIndex.StateOfMonster[2, i];
                        }
                    }
                    break;

            }
            return "";
        }

        void CheckPan()
        {
            //소환했던 거라면 취소하고 정한위치에 재소환
            for (int i = 0; i < 3; i++)
            {
                //y값 설정
                for (int j = 0; j < 7; j++)
                {
                    //x값 설정
                    if (DataIndex.pan[i, j] == DataIndex.SommonID)
                    {
                        GameObject temp = GameObject.Find("" + i + "," + j);
                        temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        DataIndex.pan[i, j] = null;
                    }
                }
            }

        }



    }
}
