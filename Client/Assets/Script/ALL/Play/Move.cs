using UnityEngine;

namespace MonsterChessClient
{
    public class Move : MonoBehaviour
    {
        public bool bPlay = false;
        public Vector2 startPos, endPos;
        public float speed = 10f;
        float time;
        Vector2 scriptEnd;

        void Update()
        {
            if (bPlay)
            {
                time += Time.deltaTime;
                speed += time;
                gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, speed);
            }
        }
        public void Initialisation()
        {
            bPlay = false;
            startPos = gameObject.transform.position;
            endPos = gameObject.transform.position;
            speed = 10f;
            time = 0;
            
        }
    }
}

