using System.Collections.Generic;
using UnityEngine;


namespace UnitType
{
    public abstract class RemoteUnit : Unit
    {
        public int attackDistance;

        public virtual void Wait(int playCount)
        {
            if (ID.Substring(0, 1) == "0")//원거리 일경우
            {
                GameObject target = GetTarget();
                Unit unit = target.GetComponent<Unit>();
                if (unit.Defence(ap)) RemoveUnit(target, playCount);

            }
        }

        public GameObject GetTarget()
        {
            List<GameObject> targets = new List<GameObject>();
            targets.Clear();
            for (int i = x - attackDistance; i < x + attackDistance + 1; i++)
            {
                for (int j = y - attackDistance; j < y - attackDistance + 1; j++)
                {
                    if (i > -1 && i < 7 && j > -1 && j < 7)
                    {
                        GameObject target = GameObject.Find(i + "," + j);
                        Unit unit = target.GetComponent<Unit>();
                        if (unit != null && gameObject.GetComponent<Unit>().order != unit.order)
                        {
                            targets.Add(target);//오더 값이 다른 애면 저장
                        }
                    }
                }
            }
            //저장한 리스트에서 한놈만 가져옴
            int tempNum = Random.Range(0, targets.Count - 1);
            return targets[tempNum];

        }
    }
}
