﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    public GameObject Login;
    public GameObject Main;
    public GameObject Select;
    public GameObject PnP;
    public GameObject Place;
    public GameObject Play;
    public enum SceneList : short
    {
        Login = 0,
        Main,
        Select,
        Place,
        Play
    }
    string SceneName;
    static Data DataIndex = Data.Instance;
	// Use this for initialization
	void Start () {
        SceneName = "" + SceneList.Login;
    }
	
	// Update is called once per frame
	void Update () {
       
        switch (SceneName)
        {
            case "Login":
                Login.SetActive(true);
                break;
            case "Main":
                break;
            case "Select":
                break;
            case "Place": 
                break;
            case "Play":
                break;
        }

       
	}
}
