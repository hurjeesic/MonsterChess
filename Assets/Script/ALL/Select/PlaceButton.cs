using UnityEngine;

namespace MonsterChessClient
{
    public class PlaceButton : MonoBehaviour
    {
        public void PlaceOn()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Place;
        }

        public void SelectOff()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
        }
    }
}