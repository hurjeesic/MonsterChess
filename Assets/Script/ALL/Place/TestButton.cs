using System;
using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class TestButton : MonoBehaviour
    {
        public void StartTest()
        {
            ReverseBoard();
            int count = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.board[i, j] != null)
                    {
                        if (i < 3)
                        {

                        }
                        count++;
                    }
                }
            }

            if (count == 12)
            {
                Data.Instance.bSummons = false; //다음 씬으로 넘어갈때 소환을 끈다.
                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다.");
            }
        }

        void ReverseBoard()
        {
            Debug.Log("리버스 판");
            for (int i = 0; i < 3; i++)
            {
                // y값
                for (int j = 0; j < 7; j++)
                {
                    // x값
                    if (Data.Instance.board[i, j] != null)
                    {
                        if (i <= 2)
                        {
                            int index = 6 - i;
                            Data.Instance.board[index, j] = Data.Instance.board[i, j];
                            GameObject TempUnit = GameObject.Find(index + "," + j);
                            Texture UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.board[index, j]) as Texture;

                            Unit unit = gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.board[index, j])) as Unit;
                            if (unit != null)
                            {
                                unit.order = 1;
                                unit.x = index;
                                unit.y = j;
                                unit.status = 0;
                            }

                            TempUnit.GetComponent<RawImage>().texture = UnitImage;
                            TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                        }
                    }
                }
            }
        }
    }
}