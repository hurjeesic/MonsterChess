using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginButton : MonoBehaviour {
    public GameObject NextList;
    // Use this for initialization
    public void LoginOn()
    {
        Debug.Log("LogON");
        Debug.Log(NextList.GetComponent<SceneManager>().SceneName);
        NextList.GetComponent<SceneManager>().SceneName = "Main";
        Debug.Log(NextList.GetComponent<SceneManager>().SceneName);
    }

}

