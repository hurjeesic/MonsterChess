using UnityEngine;

namespace MonsterChessClient
{
    public class SommonButtonOfPlay : MonoBehaviour
    {
        public void SommonOnPlay()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Play)
            {

            }
        }
    }
}