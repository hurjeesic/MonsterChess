using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitType;

public class Move : MonoBehaviour {
    float time = 0;
    float speed = 0;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool bPlay = false;
	// Use this for initialization
	void Start ()
    {
        //bPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bPlay)
        {
            time += Time.deltaTime;
            if (gameObject.name.Contains("CFX4 Fire")) speed += time;
            else { speed += time / 2; }
               

            gameObject.transform.position = Vector3.MoveTowards(startPos, endPos, speed);

            if (gameObject.transform.position.x == endPos.x && gameObject.transform.position.y == endPos.y)
            {
                Debug.Log("종료!");
                
                if (gameObject.name.Contains("CFX4 Fire")){ DestroyImmediate(gameObject);}
                else if (gameObject != null) DestroyImmediate(gameObject.GetComponent<Move>());
            }
           
        }
    }
}
