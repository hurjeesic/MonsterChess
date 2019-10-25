using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserData
{
    public enum RANK
    {
        UnRank = 0,

        Private, PrivateFirstClass, Corporal, Sergeant, // 훈련병=이병, 일병, 상병, 병장
        StaffSergeant, SergeantFirstClass, FirstSergeant, CommandSergeantMajor, // 하사, 중사, 상사, 원사
        WarrantOfficer, SecondLieutenant, FirstLieutenant, Captain, // 준위, 소위, 중위, 대위
        Major, LieutenantColonel, Colonel, // 소령, 중령, 대령
        BrigadierGeneral, MajorGeneral, LieutenantGeneral, General, // 준장, 소장, 중장, 대장
        President
    }

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
