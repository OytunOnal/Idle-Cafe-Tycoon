using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkTree : Producer
{
    void Start()
    {
        productName = "Milk";
        produceTime = 1;

        Init();
    }
}
