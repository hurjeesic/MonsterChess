using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserData
{
    class User
    {
        public string name { get; private set; }
        public int score { get; private set; }
        public int rank { get; private set; }

        private static User instance;

        public static User Instance
        {
            get
            {
                if (instance == null) instance = new User();

                return instance;
            }
        }

        public void Initialize(string name, int score, int rank)
        {
            this.name = name;
            this.score = score;
            this.rank = rank;
        }

        public void UpdateInfo(int score, int rank)
        {
            this.score = score;
            this.rank = rank;
        }
    }
}
