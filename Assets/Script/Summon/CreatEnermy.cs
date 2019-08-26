using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.UI;

public class CreatEnermy : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    public void Create2P() {
        ReversePan();
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (DataIndex.pan[j, i] != null)
                {
                    AddToMonsterList(j, i);
                }
            }

        }
        DataIndex.SommonON = false;
        for (int i = 0; i < DataIndex.MonsterList.Count(); i++)
        {
            Debug.Log(DataIndex.MonsterList[i]);
        }
        

        SceneManager.LoadScene("PlayGame");
      
      

    }

    void AddToMonsterList(int x, int y)
    {
        string ID;
        string ManaCost;
        string AtackPoint;
        string HelthPoint;

        string List;

        ID = DataIndex.pan[x, y];
        switch (ID.Substring(0, 1))
        {
            case "0":
                for (int i = 0; i < 12; i++)
                {
                    if (ID == DataIndex.StateOfMonster[0, i].Substring(0, 3))
                    {
                        ManaCost = DataIndex.StateOfMonster[0, i].Substring(6, 1);
                        AtackPoint = DataIndex.StateOfMonster[0, i].Substring(8, 1);
                        HelthPoint = DataIndex.StateOfMonster[0, i].Substring(7, 1);
                        List = ID + ManaCost + AtackPoint + HelthPoint;
                        Debug.Log(List);
                        DataIndex.MonsterList.Add(List);
                        break;
                    }
                }
                break;
            case "1":
                for (int i = 0; i < 12; i++)
                {
                    if (ID == DataIndex.StateOfMonster[1, i].Substring(0, 3))
                    {
                        ManaCost = DataIndex.StateOfMonster[1, i].Substring(6, 1);
                        AtackPoint = DataIndex.StateOfMonster[1, i].Substring(8, 1);
                        HelthPoint = DataIndex.StateOfMonster[1, i].Substring(7, 1);
                        List = ID + ManaCost + AtackPoint + HelthPoint;
                        Debug.Log(List);
                        DataIndex.MonsterList.Add(List);
                        break;

                    }
                }
                break;
            case "2":
                for (int i = 0; i < 12; i++)
                {
                    if (ID == DataIndex.StateOfMonster[2, i].Substring(0, 3))
                    {
                        ManaCost = DataIndex.StateOfMonster[2, i].Substring(6, 1);
                        AtackPoint = DataIndex.StateOfMonster[2, i].Substring(8, 1);
                        HelthPoint = DataIndex.StateOfMonster[2, i].Substring(7, 1);
                        List = ID + ManaCost + AtackPoint + HelthPoint;
                        Debug.Log(List);
                        DataIndex.MonsterList.Add(List);
                        break;

                    }
                }
                break;
        }
    }
    void ReversePan() {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (DataIndex.pan[j, i] != null)
                {
                   
                    GameObject temp;
                    switch (i)
                    {
                        
                        case 0:
                            DataIndex.pan[j, 6] = DataIndex.pan[j, i];
                            break;
                        case 1:
                           
                            DataIndex.pan[j, 5] = DataIndex.pan[j, i];
                            break;
                        case 2:
                           
                            DataIndex.pan[j, 4] = DataIndex.pan[j, i];
                            break;
                    }
                }
            }

        }
       
    }
}
