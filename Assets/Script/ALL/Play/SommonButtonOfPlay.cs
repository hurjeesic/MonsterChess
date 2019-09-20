using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterChessClient;

public class SommonButtonOfPlay : MonoBehaviour {

    public void SommonOnPlay()
    {
        if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Play)
        {

        }

    }
}
