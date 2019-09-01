using UnityEngine;

namespace MonsterChessClient
{
    public class NextButton : MonoBehaviour
    {
        public void PlayOn()
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Play;
        }
    }
}