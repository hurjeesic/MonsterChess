using UnityEngine;

namespace MonsterChessClient
{
    public class Move : MonoBehaviour
    {
        public bool bPlay = false;
        public Vector2 startPos, endPos;
        public float speed = 10f;
        float time;

        void Update()
        {
            if (bPlay)
            {
                time += Time.deltaTime;
                speed *= time;
                gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, speed);
            }
        }
    }
}

