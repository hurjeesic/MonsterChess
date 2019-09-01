using UnityEngine;
using System.Collections.Generic;

namespace MonsterChessClient
{
    public class ImageNumber : MonoBehaviour
    {

        List<Texture> images;
        int width;

        void Awake()
        {
            this.images = new List<Texture>();
            for (int i = 0; i <= 9; ++i)
            {
                Texture texture = Resources.Load(string.Format("images/n{0:D2}", i)) as Texture;
                this.images.Add(texture);
            }

            this.width = this.images[0].width;
        }

        public void Print(int number, float x, float y, float ratio)
        {
            string numberString = string.Format("{0:22}", number.ToString("D2"));
            for (int i = 0; i < numberString.Length; i++)
            {
                string digit = numberString.Substring(i, 1);
                PrintTexture(int.Parse(digit), x, y, ratio);
                x += this.width;
            }
        }

        void PrintTexture(int texture_index, float x, float y, float ratio)
        {
            Texture target = this.images[texture_index];
            GUI.DrawTexture(new Rect(x, y, target.width * ratio, target.height * ratio), target);
        }
    }
}