using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MonsterChessClient
{
    public class SelectSystem : MonoBehaviour
    {
        public Main main;
        public NextButton place;

        [HideInInspector]
        public List<string> monster = new List<string>();

        static Data DataIndex = Data.Instance;

        public void Enter()
        {
            monster.Clear();
        }

        public void MoveMain()
        {
            main.Enter();
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
        }

        public void AddMonster()
        {
            if ((DataIndex.Hero == false) && (DataIndex.Kind == 2))
            {
                // Debug.Log("히어로를 선택했대요"); // 시각적으로 확인 가능하므로 로그 표시 삭제
                DataIndex.Hero = true;
            }
            monster.Add(DataIndex.MonsterID);
            // Debug.Log("몬스터" + DataIndex.MonsterID + "가 추가되었습니다"); // 시각적으로 확인 가능하므로 로그 표시 삭제
        }

        public void RemoveMonster()
        {
            if ((DataIndex.Hero == true) && (DataIndex.Kind == 2))
            {
                DataIndex.Hero = false;
            }
            monster.Remove(DataIndex.MonsterID);
            // Debug.Log("몬스터" + DataIndex.MonsterID + "가 삭제되었습니다"); // 시각적으로 확인 가능하므로 로그 표시 삭제
        }

        public void Done()
        {
            if (monster.Count() == 6)
            {
                Debug.Log("선택된 몬스터");
                for (int x = 0; x < monster.Count(); x++)
                {

                    DataIndex.Unit[x] = monster[x];
                    Debug.Log(monster[x]);
                }
                Array.Sort(DataIndex.Unit);
                Debug.Log("정렬된 몬스터");
                for (int x = 0; x < monster.Count(); x++)
                {
                    Debug.Log(DataIndex.Unit[x]);
                }

                place.Enter();

                // SceneManager.LoadScene("배치");
                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Place;
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
}