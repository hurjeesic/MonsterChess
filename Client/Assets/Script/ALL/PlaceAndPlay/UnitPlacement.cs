using FreeNet;
using System;
using UnitType;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MonsterChessClient
{
    public class UnitPlacement : MonoBehaviour
    {
        public Text manaText;
        public Text unitText;

        public NetworkManager networkManager;
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
                                if (slotTrans.GetComponent<Unit>() != null) DestroyImmediate(slotTrans.GetComponent<Unit>());
                                slotTrans.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.summonId) as Texture;
                                slotTrans.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                int tempUnitCount = 0;
                                tempUnitCount = CheckUnitCount();
                                unitText.text = ""+tempUnitCount;
                                int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.summonId).Substring(6, 1));
                                Data.Instance.mana -= cost;
                                manaText.text = Data.Instance.mana + "";
                                Unit unit = slotTrans.gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.summonId)) as Unit;
                                if (unit != null)
                                {
                                    unit.order = Data.Instance.order;
                                    unit.x = x;
                                    unit.y = y;
                                    unit.status = 0;
                                }
                                Data.Instance.bSummons = false;
                            }
                        }
                    }
                    break;
                case SceneList.Play:
                    Debug.Log("플레이 입니다.");
                    if (Data.Instance.bSummons == true && Data.Instance.time <= 30)
                    {
                        Debug.Log("소환준비");

                        Vector2 unitPos;
                        x = slotTrans.name[0] - '0';
                        y = slotTrans.name[2] - '0';
                        Unit unit = slotTrans.gameObject.GetComponent(Type.GetType("UnitType.Unit" + Data.Instance.summonId)) as Unit;
                        if (unit == null)
                        {
                            Packet summon = Packet.Create((short)PROTOCOL.RequestedSummons);
                            unitPos = new Vector2(x, y);
                            summon.Push((int)unitPos.x);
                            summon.Push((int)unitPos.y);
                            summon.Push(Data.Instance.summonId);
                            this.networkManager.Send(summon);

                            Data.Instance.bSummons = false;
                        }
                        else
                        {
                            Debug.Log("소환하려는 칸에 유닛이 있습니다..");
                        }
                       
                    }
                    else if (Data.Instance.bMoving == false && Data.Instance.time <= 30)
                    {
                        // 터치한 유닛의 이동 범위를 가져옴
                        if (Data.Instance.board[x, y].Value != null)
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
                        if (Data.Instance.board[originX, originY].Value != null)
                        {
                            Unit unit = Data.Instance.origin.GetComponent<Unit>();
                            for (int i = 0; i < unit.range.Count; i++)
                            {
                                if (unit.range[i].Value == GameObject.Find(x + "," + y))
                                {
                                    //일치하는 것을 찾음
                                    unit.moveX = x;
                                    unit.moveY = y;
                                    unit.SaveMove();

                                    Packet msg = Packet.Create((short)PROTOCOL.RequestedMoving);
                                    msg.Push(originX);
                                    msg.Push(originY);
                                    msg.Push(x);
                                    msg.Push(y);
                                    networkManager.Send(msg);

                                    break;
                                }
                                if (i == unit.range.Count - 1)
                                {
                                    Debug.Log("알맞은 이동범위를 선택하시기 바랍니다.");
                                    Data.Instance.AnimStop();
                                }
                               
                            }
                           
                        }

                        Data.Instance.bMoving = false;
                    }
                    break;
            }
        }

        int CheckUnitCount()
        {
            int temp = 0;
            for (int x = 0; x < Data.COLUMN; x++)
            {
                // y값 설정
                for (int y = 0; y < Data.ROW / 2; y++)
                {
                    // x값 설정
                    Unit tempUnit = GameObject.Find(x + "," + y).GetComponent<Unit>();
                    if (tempUnit!=null) temp++;
                }
            }
            return temp;
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
                    Unit tempUnit = GameObject.Find(x + "," + y).GetComponent<Unit>();
                    if (tempUnit !=null && tempUnit.ID == Data.Instance.summonId)
                    {
                        tempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.board[x, y] = Data.Instance.Empty;
                        DestroyImmediate(tempUnit.GetComponent<Unit>());
                        int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.summonId).Substring(6, 1));
                        Data.Instance.mana += cost;
                        manaText.text = "" + Data.Instance.mana;
                    }
                }
            }
          
        }
    }
}