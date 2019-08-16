using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanUpdate : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    void Start () {
        Debug.Log("시간은" + DataIndex.Time);
    }



    // Update is called once per frame
    void Update () {
        if (DataIndex.Time >= 40)//30초 시작
        {
            //마나석 (재우)
            if(DataIndex.ManaStone == 0)
            {
                //중립지역에 새로운 마나스톤 하나 생성(성주니 화이팅)
            }
            //마나 1획득(재우)
            DataIndex.Mana++;
            GameObject ManaText = GameObject.Find("Mana");
            ManaText.GetComponent<Text>().text = DataIndex.Mana + "";

            //================================================
            DataIndex.MoveON = false;
            DataIndex.SommonON = false;
            DataIndex.Time = 31;
        }
        else if (DataIndex.Time <= 1)
        {
            DataIndex.Time = 0;
            GameObject TimeText = GameObject.Find("Time");
            TimeText.GetComponent<Text>().text = Mathf.Floor(DataIndex.Time) + "";

            //30초 끝
            //이동및 충돌(성준)
            //마나스톤의 값이 2가 되면 0으로 돌리는 프로그램(성주니)
            //시간초 초기화(재우)
            DataIndex.Time = 50;
            DataIndex.MoveON = true;
            for (int i = 0; i < 10; i++) { }
        }
        else
        {
            //시간이 진행됨
            DataIndex.Time -= Time.deltaTime;
            GameObject TimeText = GameObject.Find("Time");
            TimeText.GetComponent<Text>().text = Mathf.Floor(DataIndex.Time) + "";
            //30초 중
              //  소환(성준)
            //이동범위 표시(성준)
        }
	}
}
