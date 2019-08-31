using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour {
    public GameObject NextList;
    // Use this for initialization
    public void FightOn()
    {
        NextList.GetComponent<SceneManager>().SceneName = "Select";
    }
}
