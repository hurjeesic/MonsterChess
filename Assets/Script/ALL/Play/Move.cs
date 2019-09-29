using UnityEngine;

namespace MonsterChessClient
{
    public class Move : MonoBehaviour
    {
        public bool bPlay = false;
        public Vector2 startPos, endPos;
        public float speed = 10f;
        float time;
        string scriptEnd;

        void Update()
        {
            if (bPlay)
            {
                time += Time.deltaTime;
                speed *= time;
                gameObject.transform.position = Vector2.MoveTowards(startPos, endPos, speed);
                scriptEnd = "" + this.transform.position;
                if (scriptEnd == "" + endPos) { Destroy(gameObject.GetComponent<Move>()); }
            }
        }
    }
}

