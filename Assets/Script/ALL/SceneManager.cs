using UnityEngine;

namespace MonsterChessClient
{
    public class SceneManager : MonoBehaviour
    {
        public enum SceneList : short
        {
            Login = 0,
            Main,
            Select,
            Place,
            Play,
            Friend
        }

        public GameObject Login;
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

        static Data DataIndex = Data.Instance;

        // Use this for initialization
        void Start()
        {
            Present = SceneList.Select;
        }
    }
}