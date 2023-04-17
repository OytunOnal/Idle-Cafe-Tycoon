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
        produceTime = 2;

        Init();
        ProductNumberDecreaseEvent += CoffeeTaken;
    }

    void CoffeeTaken()
    {
        GWorld.Instance.GetWorld().ModifyState("CoffeeReady", -1);
    }

}
