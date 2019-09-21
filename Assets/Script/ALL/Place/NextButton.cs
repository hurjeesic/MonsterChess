using FreeNet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class NextButton : MonoBehaviour
    {
        public NetworkManager networkManager;
        public Text btnTxt;
        public Button cancleBtn;
        public GameObject nextBtn;
        public GameObject matchCancleBtn;
        List<Vector2> unitPos = new List<Vector2>();

        public void Enter()
        {
            networkManager.messageReceiver = this;
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
        }

        public void PlayOn()
        {
            int count = 0;
            unitPos.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Data.Instance.pan[i, j] != null)
                    {
                        unitPos.Add(new Vector2(i, j));
                        count++;
                    }
                }
            }

            if (count == 6)
            {
                Data.Instance.SommonOn = false;//다음 씬으로 넘어갈때 소환을 끈다.
                btnTxt.text = "매칭 취소";
                nextBtn.SetActive(false);
                matchCancleBtn.SetActive(true);
                cancleBtn.enabled = false;
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다. " + count + "개");
                count = 0;
            }
        }

        public void PlayOff()
        {
            btnTxt.text = "확인";
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
            cancleBtn.enabled = true;
        }
    }
}