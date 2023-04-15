using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMProducer : Producer
{
    // Start is called before the first frame update
    void Start()
    {
        hasPrequisites = true;
        productName = "Coffee";
        productWaitTime = 5;

        Init();
    }

}
