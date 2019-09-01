using System.Collections.Generic;
using UnityEngine;

namespace MonsterChessClient
{
    public class BattleInformaionPanel : MonoBehaviour
    {
        List<Texture> turnInformation;
        int width;
        int height;

        Texture myTeamMark;
        Texture otherTeamMark;

        void Awake()
        {
            this.turnInformation = new List<Texture>();
            this.turnInformation.Add(Resources.Load("images/red_playing") as Texture);
            this.turnInformation.Add(Resources.Load("images/blue_playing") as Texture);

            this.width = this.turnInformation[0].width;
            this.height = this.turnInformation[0].height;

            this.myTeamMark = Resources.Load("images/me") as Texture;
            this.otherTeamMark = Resources.Load("images/other") as Texture;
        }

        public void DrawTurnInformation(int playerIndex, float ratio)
        {
            Texture texture = this.turnInformation[playerIndex];

            Rect rect;
            if (playerIndex == 0)
            {
                rect = new Rect(0, 0, this.width * ratio, this.height * ratio);
            }
            else
            {
                rect = new Rect(Screen.width - width * ratio, 0, this.width * ratio, this.height * ratio);
            }

            GUI.DrawTexture(rect, texture);
        }


        public void DrawMyInformation(int playerMeIndex, float ratio)
        {
            Rect rectMe;
            Rect rectOther;
            if (playerMeIndex == 0)
            {
                rectMe = new Rect(0, this.height * ratio, this.myTeamMark.width * ratio, this.myTeamMark.height * ratio);
                rectOther = new Rect(Screen.width - this.otherTeamMark.width * ratio, this.height * ratio, this.otherTeamMark.width * ratio, this.otherTeamMark.height * ratio);
            }
            else
            {
                rectMe = new Rect(Screen.width - this.myTeamMark.width * ratio, this.height * ratio, this.myTeamMark.width * ratio, this.myTeamMark.height * ratio);
                rectOther = new Rect(0, this.height * ratio, this.otherTeamMark.width * ratio, this.otherTeamMark.height * ratio);
            }

            GUI.DrawTexture(rectMe, this.myTeamMark);
            GUI.DrawTexture(rectOther, this.otherTeamMark);
        }
    }
}