using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterChessClient
{
    public class SelectSystem : MonoBehaviour
    {
        public Main main;
        public NextButton place;

        [HideInInspector]
        public List<string> monster = new List<string>();

        Data instance = Data.Instance;

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
            if ((instance.bHero == false) && (instance.kind == 2))
            {
                // Debug.Log("히어로를 선택했대요"); // 시각적으로 확인 가능하므로 로그 표시 삭제
                instance.bHero = true;
            }
            monster.Add(instance.unitId);
        }

        public void RemoveMonster()
        {
            if ((instance.bHero == true) && (instance.kind == 2))
            {
                instance.bHero = false;
            }
            monster.Remove(instance.unitId);
        }

        public void Done()
        {
            if (monster.Count() == 6)
            {
                Array.Sort(instance.units);
                for (int i = 0; i < monster.Count(); i++)
                {
                    instance.units[i] = monster[i];
                }

                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Place;

                place.Enter();
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