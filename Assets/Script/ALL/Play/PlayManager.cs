using UnitType;
using System.Collections;
using UnityEngine;

namespace MonsterChessClient
{
    public class PlayManager : MonoBehaviour
    {
        int playListCount = 0;
        int tempCount = 0;

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
            int x = int.Parse(Data.Instance.playList[playListCount].name.Substring(2));
            int y = int.Parse(Data.Instance.playList[playListCount].name.Substring(0, 1));
            int moveX, moveY;
            string id = Data.Instance.board[y, x];
            int dIrection;
            int order;

            Unit unit = Data.Instance.playList[playListCount].GetComponent<Unit>();
            switch (unit.status)
            {
                case 0:
                    Debug.Log("대기");
                    break;
                case 1:
                    //이동
                    Debug.Log("이동");
                    moveX = unit.moveX;
                    moveY = unit.moveY;
                    dIrection = unit.moveDirection;
                    order = unit.order;
                    CheckMove(id, dIrection, moveX, moveY, x, y, order);

                    if (Data.Instance.board[moveY, moveX] == null) unit.Move();
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
                yield return new WaitForSeconds(3f);
                tempCount = 0;
                playListCount = 0;
                Data.Instance.bPlaying = false;
            }

            Debug.Log("끝");
        }

        private void CheckMove(string id, int moveDirection, int moveX, int moveY, int x, int y, int order)
        {
            int tempX;
            switch (moveDirection)
            {
                case 0: // 동
                    for (int i = x + 1; i < moveX + 1; i++)
                    {
                        if (i < 7 && Data.Instance.board[y, i] != null)
                        {
                            GameObject TargetObject = GameObject.Find(y + "," + i);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                            GameObject TargetObject = GameObject.Find(y + "," + i);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                            GameObject TargetObject = GameObject.Find(i + "," + x);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                        if (i < 7 && Data.Instance.board[i, x] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + x);
                            Unit unit = TargetObject.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveY = i - (unit.order == order ? 1 : 0);

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
                            GameObject TargetObject = GameObject.Find(i + "," + tempX);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                            GameObject TargetObject = GameObject.Find(i + "," + tempX);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                            GameObject TargetObject = GameObject.Find(i + "," + tempX);
                            Unit unit = TargetObject.GetComponent<Unit>();
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
                            GameObject TargetObject = GameObject.Find(i + "," + tempX);
                            Unit unit = TargetObject.GetComponent<Unit>();
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