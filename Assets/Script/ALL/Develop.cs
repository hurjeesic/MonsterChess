using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Develop : MonoBehaviour {
    public GameObject developText;
    int time;
    void Update()
    {
       time += (int)Time.deltaTime;
        if (time > 2) gameObject.SetActive(false);
    }
    public void ViewText()
    {
        developText.SetActive(true);
    }
}


