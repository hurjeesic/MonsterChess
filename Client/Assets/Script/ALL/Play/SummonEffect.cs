using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class SummonEffect : MonoBehaviour
    {
        private void Update()
        {
            //원형의 소환 이펙트가 그냥 막 존나 돌아야 해요
            float fMove = Time.deltaTime * speed;
            gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/Pan/SummonEffect") as Texture;
            transform.Rotate(new Vector2(15, 0) * fMove);
        }
        
        public int speed = 100;
        public Transform prefab;

        public void Effect(int x, int y)
        {
            Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        }
    }
}
