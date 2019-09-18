using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardUpdate : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {


        }


        // Update is called once per frame
        void Update()
        {
            if (Data.Instance.PlayOn == false)
            {
                if (Data.Instance.Time >= 40)//30초 시작
                {
                    //SortList();//무브리스트 정렬
                    Data.Instance.Mana++;
                    GameObject ManaText = GameObject.Find("Mana");
                    ManaText.GetComponent<Text>().text = Data.Instance.Mana + "";
                    //================================================
                    Data.Instance.SommonOn = false;
                    Data.Instance.MoveOn = false;
                    Data.Instance.Time = 31;

                }
                else if (Data.Instance.Time <= 0)
                {

                    Data.Instance.Time = 0;
                    GameObject TimeText = GameObject.Find("Time");
                    TimeText.GetComponent<Text>().text = Mathf.Floor(Data.Instance.Time) + "";
                    Data.Instance.Turn++;
                    //30초 끝
                    //시간초 초기화(재우)
                    Data.Instance.Time = 50;
                    Data.Instance.PlayOn = true;
                }
                else
                {
                    //시간이 진행됨
                    Data.Instance.Time -= Time.deltaTime;
                    GameObject TimeText = GameObject.Find("Time");
                    TimeText.GetComponent<Text>().text = Mathf.Floor(Data.Instance.Time) + "";
                    //30초 중
                    // 소환(성준)
                    //이동범위 표시(성준)
                }

            }


        }

    }
}
