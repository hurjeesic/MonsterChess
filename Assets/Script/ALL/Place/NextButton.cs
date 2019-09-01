using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour {
    public GameObject NextList;
    // Use this for initialization
    public void PlayOn()
    {
        NextList.GetComponent<SceneManager>().SceneName = "Play";
    }
}
