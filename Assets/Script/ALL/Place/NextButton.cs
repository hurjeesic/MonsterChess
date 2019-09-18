using System.Linq;
using UnityEngine;


namespace MonsterChessClient
{

    public class NextButton : MonoBehaviour
    {
        public void PlayOn()
        {
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Data.Instance.pan[i, j] != null)
                    {
                        count++;
                    }
                }
            }

            if (count == 6)
            {
                Data.Instance.SommonOn = false;//다음 씬으로 넘어갈때 소환을 끈다.
                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다.");
                count = 0;
            }
        }


    }


}