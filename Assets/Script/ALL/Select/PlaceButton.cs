using UnityEngine;

namespace MonsterChessClient
{
    public class PlaceButton : MonoBehaviour
    {

        public void SelectOff()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
        }
    }
}