using UnityEngine;

namespace MonsterChessClient
{
    public class PlaceButton : MonoBehaviour
    {
        public void PlaceOn()
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneList.Place;
        }

        public void SelectOff()
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneList.Main;
        }
    }
}