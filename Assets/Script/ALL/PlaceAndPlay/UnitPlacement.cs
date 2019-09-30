using System;
using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class UnitPlacement : MonoBehaviour
    {
        void Start()
        {
            Transform[] boardTrans = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform slotTrans in boardTrans)
            {
                Button btn = slotTrans.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => SommonUnit(slotTrans));
                }
            }
        }

        void SommonUnit(Transform slotTrans)
        {
            switch (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present)
            {
                case SceneList.Place:
                    if (Data.Instance.bSummons == true)
                    {
                        int x = int.Parse(slotTrans.name.Substring(2));
                        int y = int.Parse(slotTrans.name.Substring(0, 1));
                        if (y < 3)
                        {
                            if (x == 3 && y == 0)
                            {
                                Debug.Log("히어로 칸 입니다.");
                            }
                            else
                            {
                                UpdateBoard();
                                slotTrans.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.sommonId) as Texture;
                                slotTrans.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                slotTrans.gameObject.AddComponent<Move>();

                                Unit unit = slotTrans.gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.sommonId)) as Unit;
                                if (unit != null)
                                {
                                    unit.order = Data.Instance.order;
                                    unit.x = x;
                                    unit.y = y;
                                    unit.status = 0;
                                }

                                Debug.Log("Unit" + Data.Instance.sommonId + " 소환");
                                Data.Instance.board[y, x] = Data.Instance.sommonId;
                                Data.Instance.bSummons = false;
                            }
                        }
                    }
                    break;
                case SceneList.Play:
                    if (Data.Instance.bSummons == true && Data.Instance.time <= 30)
                    {
                        int x = int.Parse(slotTrans.name.Substring(2));
                        int y = int.Parse(slotTrans.name.Substring(0, 1));
                        int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.sommonId).Substring(6, 1));
                        if (Data.Instance.mana - cost >= 0)
                        {
                            if (Data.Instance.board[y, x] == null)
                            {
                                // 소환은 즉각 적용 시키고 플레이 리스트에 제외 시킨다.
                                x = int.Parse(slotTrans.name.Substring(2));
                                y = int.Parse(slotTrans.name.Substring(0, 1));
                                slotTrans.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.sommonId) as Texture;
                                slotTrans.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);

                                Data.Instance.board[y, x] = Data.Instance.sommonId;
                                Data.Instance.bSummons = false;
                                Data.Instance.mana -= cost;

                                Unit unit = slotTrans.gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.sommonId)) as Unit;
                                if (unit != null)
                                {
                                    unit.order = Data.Instance.order;
                                    unit.x = x;
                                    unit.y = y;
                                    unit.status = 2;
                                }
                            }
                            else
                            {
                                Debug.Log("배치할려는 칸에 유닛 존재");
                            }
                        }
                        else
                        {
                            Debug.Log("마나부족");
                        }
                    }
                    else if (Data.Instance.bMoving == false && Data.Instance.time <= 30)
                    {
                        // 터치한 유닛의 이동 범위를 가져옴
                        int x = int.Parse(slotTrans.name.Substring(2));
                        int y = int.Parse(slotTrans.name.Substring(0, 1));
                        if (Data.Instance.board[y, x] != null)
                        {
                            slotTrans.GetComponent<Unit>().MoveRange();
                        }

                        Data.Instance.origin = slotTrans.gameObject;
                        Data.Instance.bMoving = true;
                    }
                    else
                    {
                        int MoveX = int.Parse(slotTrans.name.Substring(2));
                        int MoveY = int.Parse(slotTrans.name.Substring(0, 1));

                        // 이동범위 클릭시 무브 리스트 변경
                        int x = int.Parse(Data.Instance.origin.name.Substring(2));
                        int y = int.Parse(Data.Instance.origin.name.Substring(0, 1));
                        if (Data.Instance.board[y, x] != null)
                        {
                            Unit unit = Data.Instance.origin.GetComponent<Unit>();
                            unit.moveX = MoveX;
                            unit.moveY = MoveY;
                            unit.SaveMove();
                        }

                        Data.Instance.bMoving = false;
                    }
                    break;
            }
        }

        void UpdateBoard()
        {
            // 소환했던 거라면 취소하고 정한위치에 재소환
            for (int i = 0; i < 3; i++)
            {
                // y값 설정
                for (int j = 0; j < 7; j++)
                {
                    // x값 설정
                    if (Data.Instance.board[i, j] == Data.Instance.sommonId)
                    {
                        GameObject.Find(i + "," + j).GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.board[i, j] = null;
                    }
                }
            }
        }
    }
}