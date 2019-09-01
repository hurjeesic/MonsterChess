using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButton : MonoBehaviour {
    public GameObject NextList;
    // Use this for initialization
    public void PlaceOn()
    {
        NextList.GetComponent<SceneManager>().SceneName = "Place";

    }
}
