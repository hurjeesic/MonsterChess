using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Select;
            monster.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    instance.In[i, j] = false;
                }
            }
            instance.MonsterCount = 0;
            instance.bHero = false;

            Data.Instance.costSum = 0;
            GameObject.Find("HeroText").GetComponent<Text>().text = "X";
            GameObject.Find("UnitText").GetComponent<Text>().text = "0";
            GameObject.Find("ManaText").GetComponent<Text>().text = Data.Instance.costSum.ToString() + "/" + Data.Instance.MaxCost;
        }

        public void MoveMain()
        {
            main.Enter();
            Data.Instance.kind = 0;
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
        }

        public void AddMonster()
        {
            if (instance.bHero == false && instance.kind == 2 && Data.Instance.MonsterCount < 6)
            {
                //히어로를 선택하지 않았을 경우 선택함
                instance.bHero = true;
            }

            monster.Add(instance.unitId);
            if(instance.kind != 2)
            {
                instance.MonsterCount++;
            }
        }

        public void RemoveMonster()
        {
            if ((instance.bHero == true) && (instance.kind == 2))
            {
                instance.bHero = false;
            }

            monster.Remove(instance.unitId);
            if (instance.kind != 2)
            {
                instance.MonsterCount--;
            }
        }

        public void Done()
        {
            if (monster.Count() == 6)
            {
                if (instance.bHero == false)
                {
                    Debug.Log("히어로를 선택 해보세요");
                }
                else
                {
                    for (int i = 0; i < monster.Count(); i++)
                    {
                        instance.units[i] = monster[i];
                    }
                    Array.Sort(instance.units);
                    for (int i = 0; i < instance.units.Length; i++)
                    {
                        Debug.Log(instance.units[i]);
                    }

                    GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Place;

                    place.Enter();
                }
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