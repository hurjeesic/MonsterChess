using UnityEngine;

namespace MonsterChessClient
{
    public static class GuiScaleModifier
    {
        static Matrix4x4 currentMatrix;
        public static void ApplyScale()
        {
            currentMatrix = GUI.matrix;
            float ratio = Screen.height / 480.0f;
            Vector3 scale = Vector3.one;
            scale.x = ratio;
            scale.y = ratio;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
        }

        public static void RestoreScale()
        {
            GUI.matrix = currentMatrix;
        }
    }
}