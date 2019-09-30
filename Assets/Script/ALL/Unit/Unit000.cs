using MonsterChessClient;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace UnitType
{
    public class Unit000 : Unit
    {
        protected override void Awake()
        {
            ID = "000";
            Distence = 1;
            Direction = 0;
            Cost = 1;
            fullHp = 2;
            ap = 1;

            status = 0;
            base.Awake();
        }
        
        public override void Attack(int playCount)
        {
            bool bDie = false;
            GameObject target = GameObject.Find(moveY + "," + moveX);
            string enemyId = Data.Instance.board[moveY, moveX];
            Unit unit = target.GetComponent(Type.GetType("UnitType.Unit" + enemyID)) as Unit;
            if (unit.Defence(ap, hp))
            {
                //destroy된 경우
                if (hp == 1) hp = 2;
                Destroy(unit);
                RemoveUnit(target, playCount);
                bDie = true;
            }
            
            if (!bDie)
            {
                Data.Instance.KnockBack(gameObject, target, moveX, moveY, x, y, moveDirection);
            }
        }

        private void RemoveUnit(GameObject target, int playCount)
        {
            Destroy(target.GetComponent<Move>());

            // Unit을 List에서 제거
            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                if (target == Data.Instance.playList[i])
                {
                    Data.Instance.playList.RemoveAt(i);
                    if (playCount > i)
                    {
                        playCount--;
                    }
                }
            }

            target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0); // Image 투명화
            Data.Instance.Move(gameObject, target, moveX, moveY, x, y); // 일반 이동
        }
    }
}