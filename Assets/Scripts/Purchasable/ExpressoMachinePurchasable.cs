using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressoMachinePurchasable : Purchasable
{
    // Start is called before the first frame update
    void Start()
    {
        price = 20;
        Init();
    }
}
