using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterChessClient
{
    public class Cancel : MonoBehaviour
    {
        public void BackStage()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Select;
        }
      
    }

}
