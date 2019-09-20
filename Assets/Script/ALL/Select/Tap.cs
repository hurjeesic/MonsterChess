using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterChessClient
{
    public class Tap : MonoBehaviour
    {
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
            Debug.Log("근거리 태비애오");
            DataIndex.Kind = 0;
        }

        public void L_Tap()
        {
            Debug.Log("원거리 태비애오");
            DataIndex.Kind = 1;
        }

        public void H_Tap()
        {
            Debug.Log("히어로 태비애오");
            DataIndex.Kind = 2;
        }
    }
}