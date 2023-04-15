using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkTree : Producer
{
    void Start()
    {
        productName = "Milk";
        productWaitTime = 1;

        Init();
    }
}
