using UnityEngine;

namespace MonsterChessClient
{
    public class SommonButton : MonoBehaviour
    {
        public void OnSommon()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                int tempNum = int.Parse(gameObject.name);
                string id = Data.Instance.units[tempNum];
                if (Data.Instance.bSommons == false)
                {
                    Data.Instance.bSommons = true;
                    Data.Instance.sommonId = id;
                    Debug.Log(Data.Instance.sommonId + " 선택");
                }
                else
                {
                    if (Data.Instance.sommonId == id)
                    {
                        Data.Instance.bSommons = false;
                    }
                    else
                    {
                        Data.Instance.bSommons = true;
                        Data.Instance.sommonId = id;
                    }
                }
            }
               
        }
    }

}
