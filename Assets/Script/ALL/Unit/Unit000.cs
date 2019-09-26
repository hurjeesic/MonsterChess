using MonsterChessClient;
using UnityEngine.UI;
using UnityEngine;

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
            Unit unit;
            switch (enemyID)
            {
                case "000":
                    unit = target.GetComponent<Unit000>();
                    if (target.GetComponent<Unit000>().Defence(ap, hp))
                    {
                        //destroy된 경우
                        if (hp == 1) hp = 2;
                        Destroy(target.GetComponent<Unit000>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "001":
                    unit = target.GetComponent<Unit001>();
                    if (target.GetComponent<Unit001>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit001>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "002":
                    unit = target.GetComponent<Unit002>();
                    if (target.GetComponent<Unit002>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit002>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "003":
                    unit = target.GetComponent<Unit003>();
                    if (target.GetComponent<Unit003>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit003>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "004":
                    unit = target.GetComponent<Unit004>();
                    if (target.GetComponent<Unit004>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit004>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "005":
                    unit = target.GetComponent<Unit005>();
                    if (target.GetComponent<Unit005>().Defence(ap, hp))
                    {
                        if (hp == 0) { }
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit005>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "006":
                    unit = target.GetComponent<Unit006>();
                    if (target.GetComponent<Unit006>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit006>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "007":
                    unit = target.GetComponent<Unit007>();
                    if (target.GetComponent<Unit007>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit007>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "008":
                    unit = target.GetComponent<Unit008>();
                    if (target.GetComponent<Unit008>().Defence(ap, hp))
                    {
                        if (hp <= 0) { }
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit008>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "009":
                    unit = target.GetComponent<Unit009>();
                    if (target.GetComponent<Unit009>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit009>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "010":
                    unit = target.GetComponent<Unit010>();
                    if (target.GetComponent<Unit010>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit010>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "011":
                    unit = target.GetComponent<Unit011>();
                    if (target.GetComponent<Unit011>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit011>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "100":
                    unit = target.GetComponent<Unit100>();
                    if (target.GetComponent<Unit100>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit100>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "101":
                    unit = target.GetComponent<Unit101>();
                    if (target.GetComponent<Unit101>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit101>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "102":
                    unit = target.GetComponent<Unit102>();
                    if (target.GetComponent<Unit102>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit102>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "103":
                    unit = target.GetComponent<Unit103>();
                    if (target.GetComponent<Unit103>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit103>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "105":
                    unit = target.GetComponent<Unit105>();
                    if (target.GetComponent<Unit105>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit105>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "200":
                    unit = target.GetComponent<Unit200>();
                    if (target.GetComponent<Unit200>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit200>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "201":
                    unit = target.GetComponent<Unit201>();
                    if (target.GetComponent<Unit201>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit201>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                case "202":
                    unit = target.GetComponent<Unit202>();
                    if (target.GetComponent<Unit202>().Defence(ap, hp))
                    {
                        //destroy된경우
                        if (hp == 1) { hp = 2; }
                        Destroy(target.GetComponent<Unit202>());
                        RemoveUnit(target, playCount);
                        bDie = true;
                    }
                    break;
                default:
                    unit = target.GetComponent<Unit000>();
                    break;
            }
            /*
            if (unit.Defence(AP, HP))
            {
                //destroy된 경우
                if (HP == 1) HP = 2;
                Destroy(Target.GetComponent<Unit>());
                RemoveUnit(Target, PlayCount);
                bDie = true;
            }
            else
            {
                Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
            }
            */
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
                if (target = Data.Instance.playList[i])
                {
                    if (playCount > i)
                    {
                        Data.Instance.playList.RemoveAt(i);
                        playCount--;
                    }
                    else
                    {
                        Data.Instance.playList.RemoveAt(i);
                    }
                }
            }

            target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0); // Image 투명화
            Data.Instance.Move(gameObject, target, moveX, moveY, x, y); // 일반 이동
        }
    }
}