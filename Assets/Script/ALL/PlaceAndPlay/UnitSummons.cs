using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class UnitSummons : MonoBehaviour
    {
        void Start()
        {
            Transform[] unitBtnTrans = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform slotTrans in unitBtnTrans)
            {
                Button btn = slotTrans.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => SelectSummons(slotTrans));
                }
            }
        }

        void SelectSummons(Transform slotTrans)
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                int tempNum = int.Parse(slotTrans.name);
                string id = Data.Instance.units[tempNum];
                if (Data.Instance.bSummons == false)
                {
                    Data.Instance.bSummons = true;
                    Data.Instance.sommonId = id;
                    Debug.Log(Data.Instance.sommonId + " 선택");
                }
                else
                {
                    if (Data.Instance.sommonId == id)
                    {
                        Data.Instance.bSummons = false;
                    }
                    else
                    {
                        Data.Instance.bSummons = true;
                        Data.Instance.sommonId = id;
                    }
                }
            }
               
        }
    }
}
