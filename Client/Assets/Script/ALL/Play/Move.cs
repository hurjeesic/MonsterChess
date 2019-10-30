using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    float time = 0;
    float speed = 0;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool bPlay = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
        time += Time.deltaTime;
        speed += time / 2;

        gameObject.transform.position = Vector3.MoveTowards(startPos, endPos, speed);
        if (gameObject.transform.position.x == endPos.x && gameObject.transform.position.y == endPos.y)
        {
            Debug.Log("종료!");
            DestroyImmediate(gameObject.GetComponent<Move>());
        }
    }
}
