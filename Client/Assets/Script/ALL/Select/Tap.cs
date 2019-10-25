using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Tap : MonoBehaviour
    {
        public Image tapS;
        public Image tapL;
        public Image tapH;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        static Data DataIndex = Data.Instance;

        public void S_Tap()
        {
            tapS.color = new Color(179/255 ,230/255,1);
            tapL.color = new Color(1, 1, 1);
            tapH.color = new Color(1, 1, 1);
            Debug.Log("근거리 태비애오");
            DataIndex.kind = 0;
        }

        public void L_Tap()
        {
            tapS.color = new Color(1, 1, 1);
            tapL.color = new Color(179/255, 230/255, 1);
            tapH.color = new Color(1, 1, 1);
            Debug.Log("원거리 태비애오");
            DataIndex.kind = 1;
        }

        public void H_Tap()
        {
            tapS.color = new Color(1, 1, 1);
            tapL.color = new Color(1, 1, 1);
            tapH.color = new Color(179/255 , 230/255 , 1);
            Debug.Log("히어로 태비애오");
            DataIndex.kind = 2;
        }
    }
}