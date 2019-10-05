using UnityEngine;
using MonsterChessClient;
namespace UnitType
{
    public class Unit200 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "200";
            Distence = 3;
            Direction = 1;
            Cost = 0;
            fullHp = 5;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp))
            {
                //디스트로이 = 데이터를 삭제하고 단순이
                RemoveUnit(target, playCount);
                if (hp <= 0) RemoveUnit(gameObject, playCount);//반격을 당하여 hp가 0이될경우
                else Data.Instance.Move(gameObject, target, moveX, moveY, x, y); // 일반 이동

            }
            else
            {
                //넉백확인후 이동(넉백함수)
                if (hp <= 0) RemoveUnit(gameObject, playCount);//반격을 당하여 hp가 0이될경우
                else Data.Instance.KnockBack(gameObject, target, moveX, moveY, x, y, moveDirection); // 넉백
            }
            Thunder(target, playCount);
        }
        public void Thunder(GameObject Target, int playCount)
        {
            int tempX = moveX;
            switch (moveDirection)
            {
                case 0:
                    for (int i = moveX + 1; i < moveX + 4; i++)
                    {
                        if (i > -1 && i < 7)
                        {
                            GameObject tempTarget = GameObject.Find(i + "," + moveY);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                            
                        }
                    }
                    break;
                case 1:
                    for (int i = moveX - 1; i > moveX - 4; i--)
                    {
                        if (i > -1 && i < 7)
                        {
                            GameObject tempTarget = GameObject.Find(i + "," + moveY);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = moveY - 1; i > moveY - 4; i--)
                    {
                        if (i > -1 && i < 7)
                        {
                            GameObject tempTarget = GameObject.Find(moveX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 3:
                    for (int i = moveY + 1; i < moveY + 4; i++)
                    {
                        if (i > -1 && i < 7)
                        {
                            GameObject tempTarget = GameObject.Find(moveX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 4:
                    for (int i = moveY + 1; i < moveY + 4; i++)
                    {
                        tempX++;
                        if (i > -1 && i < 7&&tempX > -1 && tempX < 7)
                        {
                            GameObject tempTarget = GameObject.Find(tempX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 5:
                    for (int i = moveY - 1; i > moveY - 4; i--)
                    {
                        tempX++;
                        if (i > -1 && i < 7 && tempX > -1 && tempX < 7)
                        {
                            GameObject tempTarget = GameObject.Find(tempX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 6:
                    for (int i = moveY - 1; i > moveY - 4; i--)
                    {
                        tempX--;
                        if (i > -1 && i < 7 && tempX > -1 && tempX < 7)
                        {
                            GameObject tempTarget = GameObject.Find(tempX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
                case 7:
                    for (int i = moveY - 1; i > moveY - 4; i--)
                    {
                        tempX++;
                        if (i > -1 && i < 7 && tempX > -1 && tempX < 7)
                        {
                            GameObject tempTarget = GameObject.Find(tempX + "," + i);
                            Unit tempUnit = tempTarget.GetComponent<Unit>();
                            if (tempUnit != null && order != tempUnit.order)
                            {
                                tempUnit.hp -= 3;
                                if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                            }
                        }
                    }
                    break;
            }
        }
    }

}