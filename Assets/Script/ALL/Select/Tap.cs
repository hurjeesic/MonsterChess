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

    static Data DataIndex = Data.Instance;

    public void S_Tap()
    {
        DataIndex.Kind = 0;
    }

    public void L_Tap()
    {
        DataIndex.Kind = 1;
    }

    public void H_Tap()
    {
        DataIndex.Kind = 2;
    }
}
