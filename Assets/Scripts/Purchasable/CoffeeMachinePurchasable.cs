using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachinePurchasable : Purchasable
{
    // Start is called before the first frame update

    void Start()
    {
        price = 10;
        Init();
    }
}
