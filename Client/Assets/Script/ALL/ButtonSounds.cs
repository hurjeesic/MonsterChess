﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ButtonSounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClick()
    {
        GetComponent<AudioSource>().Play();
        //audio.clip = Sword;
    }

}
