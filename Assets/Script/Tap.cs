using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static MonsterData test1 = MonsterData.Instance;

    public void S_Tap()
    {
        test1.Kind = 0;
    }

    public void L_Tap()
    {
        test1.Kind = 1;
    }

    public void H_Tap()
    {
        test1.Kind = 2;
    }
}
