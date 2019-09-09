using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<string> monster = new List<string>();

    static Data DataIndex = Data.Instance;



    public void AddMonster()
    {
        monster.Add(DataIndex.MonsterID);
        Debug.Log("몬스터" + DataIndex.MonsterID + "가 추가되었습니다");
    }

    public void RemoveMonster()
    {
        monster.Remove(DataIndex.MonsterID);
        Debug.Log("몬스터" + DataIndex.MonsterID + "가 삭제되었습니다");

    }

    public void Done()
    {
        if (monster.Count() == 6)
        {
            for (int x = 0; x < monster.Count(); x++)
            {

                DataIndex.Unit[x] = monster[x];
                Debug.Log(monster[x]);
            }
            Array.Sort(DataIndex.Unit);
            for (int x = 0; x < monster.Count(); x++)
            {
                Debug.Log(DataIndex.Unit[x]);
            }
            SceneManager.LoadScene("배치");
        }
        else if (monster.Count < 6)
        {
            Debug.Log(6 - (monster.Count()) + "개를 더 골라야 합니다");
        }
        else
        {
            Debug.Log((monster.Count() - 6) + "개를 빼야 합니다");
        }
    }
}
