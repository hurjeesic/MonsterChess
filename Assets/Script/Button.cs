using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    string[] ShortMonsterArray = {"002","005","008","010"};
    string[] LongMonsterArray = {"101","102","103","104"};
    string[] HeroMonsterArray = {"200","201","202","203"};

    bool[,] In = { {false, false, false, false }, { false, false, false, false }, { false, false, false, false } };

    int N = 0;
    int a = 0;
    string temp;

    static MonsterData DataIndex = MonsterData.Instance;
    

    void Start()
    {
        a = int.Parse(this.name);
        ChoiceImage(N);
        Debug.Log(In[N, a]);
    }

    // Update is called once per frame

    void Update()
    {
        if (DataIndex.Kind != N)
        {
            N = DataIndex.Kind;
            ChoiceImage(N);
        }
        if (In[N, a] == false)
        {
            gameObject.GetComponent<RawImage>().color
                = new Color(191, 191, 191, 255);
            //나중에 선택된 이미지 넣기
        }
        else
        {
            gameObject.GetComponent<RawImage>().color 
                = new Color(200, 200, 200, 255);
        }
    }

    public void OnClick()
    {
        switch (N)
        {
            case 0:
                DataIndex.MonsterID = ShortMonsterArray[a];
                break;
            case 1:
                DataIndex.MonsterID = LongMonsterArray[a];
                break;
            case 2:
                DataIndex.MonsterID = HeroMonsterArray[a];
                break;
        }
        if (In[N,a] == true)
        {
            DataIndex.Cost -= int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));

            GameObject.Find("MainSystem").GetComponent<Main>().RemoveMonster();
            In[N, a] = !In[N, a];
        }
        else
        {
            DataIndex.Cost += int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));
            if (DataIndex.Cost > 10)
            {
                Debug.Log("마나코스트의 합이 10을 넘겼습니다");
                DataIndex.Cost -= int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));
            }
            else
            {
                GameObject.Find("MainSystem").GetComponent<Main>().AddMonster();
                In[N, a] = !In[N, a];
            }
        }
        Debug.Log("현재의 마나코스트는" + DataIndex.Cost + "입니다");
    }

    public void ChoiceImage(int x)
    {
        switch(x)
        {
            case 0:
                temp = "Image/Unit/" + ShortMonsterArray[a];
                gameObject.GetComponent<RawImage>().texture = Resources.Load(temp)as Texture;
                break;
            case 1:
                temp = "Image/Unit/" + LongMonsterArray[a];
                gameObject.GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
                break;
            case 2:
                temp = "Image/Unit/" + HeroMonsterArray[a];
                gameObject.GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
                break;
        }
        

    }
}
