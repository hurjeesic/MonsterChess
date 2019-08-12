using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanUpdate : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (DataIndex.TIme == 0)
        {
            //30초 스타트 
            //마나 1획득(재우)
            //시간초 시작(재우)
            DataIndex.MoveON = false;
            DataIndex.SommonON = false;
        }
        else if (DataIndex.TIme == 30)
        {
            //30초 끝
            //이동및 충돌(성준)
            //마나석(재우)
            //시간초 초기화(재우)
            DataIndex.MoveON = true;
            for (int i = 0; i < 10; i++) { }
        }
        else
        {
            //30초 중
              //  소환(성준)
            //이동범위 표시(성준)
        }
	}
}
