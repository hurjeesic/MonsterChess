using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CheckButton : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
   
    public void OnCheck() {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (DataIndex.pan[i, j] != null)
                {
                    AddToMonsterList(i,j);
                }
            }

        }
        if (DataIndex.MonsterList.Count() == 6)
        {
            DataIndex.SommonON = false;
            SceneManager.LoadScene("게임판");     
        }
        else
        {
            Debug.Log("리스트의 갯수가 모자랍니다.");
            DataIndex.MonsterList.Clear();
        }

    }
    public void OnReturn() {
        SceneManager.LoadScene("선택");
    }

    void AddToMonsterList(int x, int y){
        string ID;
        string ManaCost;
        string AtackPoint;
        string HelthPoint;
   
        string List;

        ID = DataIndex.pan[x, y];
        switch (ID.Substring(0,1))
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
}
