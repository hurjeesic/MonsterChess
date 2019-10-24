using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class ManaArea : MonoBehaviour
    {

        Data DataIndex = Data.Instance;

        int MAreaS = 0;
        int MSA = 0;//이건 나중에 서버에서 받는 값으로 바꿔주면 대요
        int Out = 10;
        bool resule = false;//0 : 없음, 1 : 있음
        bool PTeam = true;//이전에 점령해 있던 팀
        bool NTeam = true;//이번턴에 점령해 있는팀

        /*
         턴이 시작할때 마나스톤이 존재하는지 확인한 후 마나스톤 생성
         CreateManaArea()

         턴이 끝날때 현재 마나스톤스택이 몇인지 확인
         마나스톤 스택이 2일 경우 점령중인 팀에게 마나석 1개를 추가후 마나석 파괴
         if(MAreaS == 2)
         {
            ManaStone++;
            DestroyManaArea;
         }

         2가 아닐경우 마나스택1을 올림
         if(CheckTeam(x,y))
         {
            MAreaS++;
         }

         바로 전에 점령하던 팀과 다른 팀 일경우 마나스택을 0으로 돌림
          if(PTeam =! NTeam)
          {
            MAreaS = 0;
          }
         */

        void CreateManaArea()
        {
            //서버에서 받은 값으로 마나스톤의 위치를 정해봐요
            MSA = 0;//나중에 0을 서버에서 보내주는 값으로 해줘요

            //서버에서 보내준 마나스톤의 위치 넣기
            transform.position = new Vector2(MSA, 3);
        }

        void DestroyManaArea()
        {
            transform.position = new Vector2(Out, Out);
        }

        //정해진 위치에 어느 팀이 있는지 구하는 함수

        int CheckTeam(int x, int y)
        {
            int Aws = 0;
            Aws = 0;//X, Y 위치에 어느팀이 있는지 봐야 해요;

            if (CheckOn(x, y))
            {
                if(DataIndex.order == 1/*어느팀인지 어캐 알아요..?*/)//x,y 칸에 어느팀이 있는지 찾어야 해요
                {
                    //x,y 위에 아군일 경우
                    Aws = 1;
                }
                else
                {
                    //x, y 위에 적군일 경우
                    Aws = 2;
                }
            }
            else
            {
                Aws = 0;
            }

            //0 : 없음, 1 : 아군, 2 : 적군
            return Aws;
        }

        bool CheckOn(int x, int y)
        {
            //누군가가 지정된 장소 위에 있는지 확인 하는것 어느 팀인지는 상관 없음
            //존재하면 true 없으면 false
            return resule;
        }
    }
}