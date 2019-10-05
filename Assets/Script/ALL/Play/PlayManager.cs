using UnitType;
using System.Collections;
using UnityEngine;

namespace MonsterChessClient
{
    public class PlayManager : MonoBehaviour
    {
        int playListCount = 0;
        int tempCount = 0;
        int moveX, moveY;
        void Update()
        {
            if (Data.Instance.bPlaying == true)
            {
                if (tempCount == playListCount)
                {
                    StartCoroutine("Play");
                    tempCount++;
                }
            }
        }

        IEnumerator Play()
        {
            Debug.Log(playListCount);
            yield return new WaitForSeconds(1f);
            int x = Data.Instance.playList[playListCount].name[0] - '0';
            int y = Data.Instance.playList[playListCount].name[2] - '0';
            
            string id = Data.Instance.board[x, y];
            int direction;
            int order;

            Unit unit = Data.Instance.playList[playListCount].GetComponent<Unit>();
            switch (unit.status)
            {
                case 0:
                    Debug.Log("대기");
                    unit.Wait(playListCount);
                    break;
                case 1:
                    //이동
                    Debug.Log("이동");
                    moveX = unit.moveX;
                    moveY = unit.moveY;
                    direction = unit.moveDirection;
                    Debug.Log("방향은" + direction);
                    order = unit.order;

                    CheckMove(id, direction, x, y, order);
                    unit.moveX = moveX;
                    unit.moveY = moveY;
                    if (Data.Instance.board[moveX, moveY] == null) unit.Move();
                    else if (moveX == x && moveY == y) unit.Wait(playListCount);
                    else unit.Attack(playListCount);
                    break;
                default:
                    // 소환 아무것도 안함
                    break;
            }

            yield return new WaitForSeconds(1f);
            playListCount++;
            if (playListCount == Data.Instance.playList.Count - 1)
            {
                for (int i = 0; i < Data.Instance.playList.Count; i++)
                {
                    Unit stateUnit = Data.Instance.playList[i].GetComponent<Unit>();
                    if (stateUnit.stateCount > 0) stateUnit.HaveState(i);
                }
                yield return new WaitForSeconds(3f);
                tempCount = 0;
                playListCount = 0;
                Data.Instance.playList.Clear();
                Data.Instance.bPlaying = false;
            }

            Debug.Log("끝");
        }

        private void CheckMove(string id, int moveDirection, int x, int y, int order)
        {
            int tempX;
            GameObject targetObj;
            Unit unit;
            switch (moveDirection)
            {
                case 0: // 동
                    for (int i = x + 1; i < moveX + 1; i++)
                    {
                        if (i < 7 && Data.Instance.board[y, i] != null)
                        {
                            targetObj = GameObject.Find(y + "," + i);
                            unit = targetObj.GetComponent<Unit>();
                            
                            if (unit != null)
                            {
                                moveX = i - (unit.order == order ? 1 : 0);
                                break;
                            }
                        }
                    }
                    break;
                case 1: // 서
                    for (int i = x - 1; i > moveX - 1; i--)
                    {
                        if (i >= 0 && Data.Instance.board[y, i] != null)
                        {
                            targetObj = GameObject.Find(y + "," + i);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveX = i + (unit.order == order ? 1 : 0);

                                break;
                            }
                        }
                    }
                    break;
                case 2: // 남
                    for (int i = y - 1; i > moveY - 1; i--)
                    {
                        if (i >= 0 && Data.Instance.board[i, x] != null)
                        {
                            targetObj = GameObject.Find(i + "," + x);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveY = i + (unit.order == order ? 1 : 0);

                                break;
                            }
                        }
                    }
                    break;
                case 3: // 북
                    for (int i = y + 1; i < moveY + 1; i++)
                    {
                        Debug.Log("for안");
                        if (i < 7 && Data.Instance.board[i, x] != null)
                        {
                            Debug.Log("들어왓따");
                            targetObj = GameObject.Find(i + "," + x);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveY = i - (unit.order == order ? 1 : 0);
                                Debug.Log(moveY);
                                break;
                            }
                        }
                    }
                    break;
                case 4: // 북동
                    tempX = x + 1;
                    for (int i = y + 1; i < moveY + 1; i++)
                    {
                        if (i < 7 && tempX < 7 && Data.Instance.board[i, tempX] != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i - (flag ? 1 : 0);
                                moveX = tempX - (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX++;
                    }
                    break;
                case 5: // 남동
                    tempX = x + 1;
                    for (int i = y - 1; i > moveY; i--)
                    {
                        if (i >= 0 && tempX < 7 && Data.Instance.board[i, tempX] != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i + (flag ? 1 : 0);
                                moveX = tempX - (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX++;
                    }
                    break;
                case 6: // 남서
                    tempX = x - 1;
                    for (int i = y - 1; i > moveY; i--)
                    {
                        if (i >= 0 && tempX >= 0 && Data.Instance.board[i, tempX] != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i + (flag ? 1 : 0);
                                moveX = tempX + (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX--;
                    }
                    break;
                case 7: // 북서
                    tempX = x - 1;
                    for (int i = y + 1; i < moveY; i++)
                    {
                        if (i < 7 && tempX >= 0 && Data.Instance.board[i, tempX] != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i - (flag ? 1 : 0);
                                moveX = tempX + (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX--;
                    }
                    break;
            }
            
        }
    }
}