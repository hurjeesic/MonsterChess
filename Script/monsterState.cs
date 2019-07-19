using UnityEngine;
using System.Collections;

public class MonsterData
{

    private static MonsterData instance = null;
    public static MonsterData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterData();
            }
            return instance;
        }
    }

    private MonsterData()
    {
    }
    public string[,] StateOfMonster = { {"000110121고블린"},{"001110121벤시"},{"002100121놀"},{"003210222드레이크"},{"004110232오크"}
,{"005100232골렘"},{"006210342트윈헤드 오우거"},{"007200333광전사"},{"008110332웜"},{"009210443데스나이트"}
,{"010210434발키리"},{"011210443용사"},{"100112221사냥꾼"},{"101112222정령궁수"},{"102112332마법사"},{"103112322대포"}
,{"104112433히드라"},{"105112442주작"},{"200320053제우스"},{"201310053아누비스"},{"202210054해태"} };


}


/*사용 방법
 
     쓰고싶은 cs파일안에 들어가 객체 선언을 합니다.
     static MonsterData test1=MonsterData.Instance;
     쓰고싶은 문자열을 지정합니다.
     string s1 = test1.stateMonster[0,0].Substring(0,3);
     Substring은 지정된 문자열을 빼오는 겁니다.
     위에는 몬스터 배열 0,0,칸에 있는 몬스터 ID를 빼오는 코드입니다. 
     
     */
