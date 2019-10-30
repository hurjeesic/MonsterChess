using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class SummonEffect : MonoBehaviour
    {

        public int speed = 1;
        public Transform prefab;

        void Effect(int x, int y)
        {
            //Instantiate(prefab, new vector2(x, y), Quaternion.identity);
            //원형의 소환 이펙트가 그냥 막 존나 돌아야 해요
            gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/Pan/SummonEffect") as Texture;
            transform.Rotate(new Vector2(15, 0) * Time.deltaTime);
            Invoke("DEft", 2f);
        }

        void DEft()
        {

        }
    }
}
