﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterChessClient
{
    public class Select : MonoBehaviour
    {
        public void MoveSelect()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Select;
        }
    }
}