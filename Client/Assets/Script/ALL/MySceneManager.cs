using FreeNet;
using System;
using System.Collections.Generic;
using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public enum SceneList : short
    {
        Login = 0,
        Account,
        FindUser,
        Main,
        Select,
        Place,
        Play,
        Friend
    }

    public class MySceneManager : MonoBehaviour
    {
        public GameObject Login;
        public GameObject Account;
        public GameObject FindUser;
        public GameObject Main;
        public GameObject Select;
        public GameObject PnP;
        public GameObject Place;
        public GameObject Play;
        public GameObject Friend;

        private SceneList present;
        public SceneList Present
        {
            get { return present; }
            set
            {
                present = value;
                switch (present)
                {
                    case SceneList.Login:
                        Login.SetActive(true);
                        FindUser.SetActive(false);
                        Account.SetActive(false);

                        Main.SetActive(false);
                        Select.SetActive(false);
                        PnP.SetActive(false);
                        Friend.SetActive(false);
                        break;
                    case SceneList.Account:
                        Login.SetActive(true);
                        FindUser.SetActive(false);
                        Account.SetActive(true);

                        Main.SetActive(false);
                        Select.SetActive(false);
                        PnP.SetActive(false);
                        Friend.SetActive(false);
                        break;
                    case SceneList.FindUser:
                        Login.SetActive(true);
                        FindUser.SetActive(true);
                        Account.SetActive(false);

                        Main.SetActive(false);
                        Select.SetActive(false);
                        PnP.SetActive(false);
                        Friend.SetActive(false);
                        break;
                    case SceneList.Main:
                        Login.SetActive(false);
                        Main.SetActive(true);
                        Select.SetActive(false);
                        PnP.SetActive(false);
                        Friend.SetActive(false);
                        break;
                    case SceneList.Select:


                        Login.SetActive(false);
                        Main.SetActive(false);
                        Select.SetActive(true);
                        PnP.SetActive(false);
                        Friend.SetActive(false);
                        break;
                    case SceneList.Place:
                      
                        Login.SetActive(false);
                        Main.SetActive(false);
                        Select.SetActive(false);

                        PnP.SetActive(true);
                        Place.SetActive(true);
                        Play.SetActive(false);

                        Friend.SetActive(false);
                        break;
                    case SceneList.Play:
                        Login.SetActive(false);
                        Main.SetActive(false);
                        Select.SetActive(false);

                        PnP.SetActive(true);
                        Place.SetActive(false);
                        Play.SetActive(true);

                        Friend.SetActive(false);
                        break;
                    case SceneList.Friend:
                        Login.SetActive(false);
                        Main.SetActive(false);
                        Select.SetActive(false);
                        PnP.SetActive(false);

                        Friend.SetActive(true);
                        break;
                }
            }
        }

      

       

        // Use this for initialization
        void Start()
        {

            Present = SceneList.Login;

        }
    }
}