using System.Linq;
using UnityEngine;


namespace MonsterChessClient
{
   
    public class NextButton : MonoBehaviour
    {
        public void PlayOn()
        {
            for (int i = 0; i < 7; i++)
            {
                //y값
                for (int j = 0; j < 7; j++)
                {
                    //x값
                    if (Data.Instance.pan[i, j] != null)
                    {
                        AddToMonsterList(i, j);
                    }
                }

            }
            if (Data.Instance.MonsterList.Count() == 6)
            {
                Data.Instance.SommonOn = false;//다음 씬으로 넘어갈때 소환을 끈다.
                GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Play;
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다.");
                Data.Instance.MonsterList.Clear();
            }
           
        }


        void AddToMonsterList(int y, int x)
        {
            for (int i = 0; i < Data.Instance.MonsterList.Count; i++)
            {
                string ID = Data.Instance.pan[y, x];
                switch (ID.Substring(0, 1))
                {
                    case "0":
                        for (int j = 0; j < 12; j++)
                        {
                            if (ID == Data.Instance.StateOfMonster[0, j].Substring(0, 3))
                            {
                                Data.Instance.MonsterList.Add(Data.Instance.StateOfMonster[0, j]);
                                break;
                            }
                        }
                        break;
                    case "1":
                        for (int j = 0; j < 6; j++)
                        {
                            if (ID == Data.Instance.StateOfMonster[1, j].Substring(0, 3))
                            {
                                Data.Instance.MonsterList.Add(Data.Instance.StateOfMonster[1, j]);
                                break;
                            }
                        }
                        break;
                    case "2":
                        for (int j = 0; j < 3; j++)
                        {
                            if (ID == Data.Instance.StateOfMonster[2, j].Substring(0, 3))
                            {
                                Data.Instance.MonsterList.Add(Data.Instance.StateOfMonster[2, j]);
                                break;
                            }
                        }
                        break;

                }
            }
        }
        void ReversePan()
        {
            for (int i = 0; i < 3; i++)
            {
                //y값
                for (int j = 0; j < 7; j++)
                {
                    //x값
                    if (Data.Instance.pan[i, j] != null)
                    {
                        switch (i)
                        {
                            case 0:
                                Data.Instance.pan[6, j] = Data.Instance.pan[i, j];
                                break;
                            case 1:
                                Data.Instance.pan[5, j] = Data.Instance.pan[i, j];
                                break;
                            case 2:
                                Data.Instance.pan[4, j] = Data.Instance.pan[i, j];
                                break;
                        }
                    }
                }

            }

        }
    }


}