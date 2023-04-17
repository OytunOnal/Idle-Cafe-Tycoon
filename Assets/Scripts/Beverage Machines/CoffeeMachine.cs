using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeMachine : BeverageMachine
{
    [SerializeField] CoffeeMachineConsumer CoffeeMachineConsumer;

    private void Start() 
    {        
        CoffeeMachineConsumer.PrequisiteFilledEvent+= PrequisiteFilled;
        productName = "Coffee";
        produceTime = 2;
    }

    protected override void BeverageReady()
    {
        GWorld.Instance.GetQueue("beverages").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("CoffeeReady", +1);

        if (!productHolder.IsFull)   
        {
            CoffeeMachineConsumer.Reset();
        }
        else
        {
            ProductNumberDecreaseEvent += CoffeeMachineConsumer.Reset;
        }
    }
}
