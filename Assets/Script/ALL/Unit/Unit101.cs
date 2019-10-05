using System.Collections.Generic;
using UnityEngine;
namespace UnitType
{
    public class Unit101 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "101";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 2;
            ap = 2;
            base.Awake();
        }
        int attackStyle = 1;
        public override void Wait(int playCount)
        {
            switch (attackStyle)
            {
                case 1:
                    GameObject target = GetTarget();
                    Unit unit = target.GetComponent<Unit>();
                    if (unit.Defence(ap, hp)) RemoveUnit(target, playCount);
                    else
                    {
                        unit.stateCount = 2;
                        unit.stateAp = 1;
                    }
                    attackStyle++;
                    break;
                case 2:
                    target = GetHealUnit();
                    unit = target.GetComponent<Unit>();
                    if (unit!=null&&unit.hp < unit.fullHp) unit.hp++;
                    attackStyle++;
                    break;
                case 3:
                    target = GetTarget();
                    unit = target.GetComponent<Unit>();
                    if (unit.Defence(ap, hp)) RemoveUnit(target, playCount);
                    else
                    {
                        unit.bmove = false;
                    }
                    attackStyle++;
                    break;
                case 4:
                    target = GetTarget();
                    unit = target.GetComponent<Unit>();
                    if (unit.Defence(ap+1, hp)) RemoveUnit(target, playCount);
                    attackStyle=1;
                    break;
            }
           
        }
        public override void Move()
        {
            if (attackStyle == 4) attackStyle = 1;
            else attackStyle++;
            base.Move();
        }
        public GameObject GetHealUnit()
        {
            List<GameObject> targets = new List<GameObject>();
            targets.Clear();
            for (int i = x - attackDistance; i < x + attackDistance + 1; i++)
            {
                for (int j = y - attackDistance; j < y - attackDistance + 1; j++)
                {
                    if (i > 0 && i < 7 && j > 0 && j < 7)
                    {
                        GameObject target = GameObject.Find(i + "," + j);
                        Unit unit = target.GetComponent<Unit>();
                        if (unit != null && gameObject.GetComponent<Unit>().order == unit.order) targets.Add(target);//오더 값이 같은 애면 저장
                    }
                        
                }
            }
            //저장한 리스트에서 한놈만 가져옴
            int tempNum = Random.Range(0, targets.Count - 1);
            return targets[tempNum];

        }
    }
}