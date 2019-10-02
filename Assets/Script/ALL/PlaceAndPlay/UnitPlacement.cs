﻿using System;
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
                    btn.onClick.AddListener(() => SummonUnit(slotTrans));
                }
            }
        }

        void SummonUnit(Transform slotTrans)
        {
            int x = slotTrans.name[0] - '0';
            int y = slotTrans.name[2] - '0';
            switch (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present)
            {
                case SceneList.Place:
                    if (Data.Instance.bSummons == true)
                    {
                        if (y < 3)
                        {
                            if (x == 3 && y == 0)
                            {
                                Debug.Log("히어로 칸 입니다.");
                            }
                            else
                            {
                                UpdateBoard();
                                slotTrans.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.summonId) as Texture;
                                slotTrans.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                slotTrans.gameObject.AddComponent<Move>();

                                Unit unit = slotTrans.gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.summonId)) as Unit;
                                if (unit != null)
                                {
                                    unit.order = Data.Instance.order;
                                    unit.x = x;
                                    unit.y = y;
                                    unit.status = 0;
                                }

                                Data.Instance.board[x, y] = Data.Instance.summonId;
                                Data.Instance.bSummons = false;
                            }
                        }
                    }
                    break;
                case SceneList.Play:
                    if (Data.Instance.bSummons == true && Data.Instance.time <= 30)
                    {
                        int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.summonId).Substring(6, 1));
                        if (Data.Instance.mana - cost >= 0)
                        {
                            if (Data.Instance.board[x, y] == null)
                            {
                                // 소환은 즉각 적용 시키고 플레이 리스트에 제외 시킨다.
                                slotTrans.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.summonId) as Texture;
                                slotTrans.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);

                                Data.Instance.board[x, y] = Data.Instance.summonId;
                                Data.Instance.bSummons = false;
                                Data.Instance.mana -= cost;

                                Unit unit = slotTrans.gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.summonId)) as Unit;
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
                        if (Data.Instance.board[x, y] != null)
                        {
                            slotTrans.GetComponent<Unit>().MoveRange();
                        }

                        Data.Instance.origin = slotTrans.gameObject;
                        Data.Instance.bMoving = true;
                    }
                    else
                    {
                        // 이동범위 클릭시 무브 리스트 변경
                        int originX = Data.Instance.origin.name[0] - '0';
                        int originY = Data.Instance.origin.name[2] - '0';
                        if (Data.Instance.board[originX, originY] != null)
                        {
                            Unit unit = Data.Instance.origin.GetComponent<Unit>();
                            unit.moveX = x;
                            unit.moveY = y;
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
            for (int x = 0; x < Data.COLUMN; x++)
            {
                // y값 설정
                for (int y = 0; y < Data.ROW / 2; y++)
                {
                    // x값 설정
                    if (Data.Instance.board[x, y] == Data.Instance.summonId)
                    {
                        GameObject.Find(x + "," + y).GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.board[x, y] = null;
                    }
                }
            }
        }
    }
}