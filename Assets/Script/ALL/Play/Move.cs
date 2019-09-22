using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MonsterChessClient
{
    public class Move : MonoBehaviour
    {
        public bool play = false;
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public float speed = 10f;
        float time;
    // Use this for initialization
        void Start()
        {

        }

    // Update is called once per frame
         void Update()
        {
            if (play == true)
            {
                time += Time.deltaTime;
                speed = speed * time;
                gameObject.transform.position = Vector2.MoveTowards(StartPosition, EndPosition, speed);
            }
           
        }
    }
}

