using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExpressoMachine : BeverageMachine
{
    [SerializeField] ExpressoMachineConsumer ExpressoMachineConsumer;

    private void Start() 
    {        
        ExpressoMachineConsumer.PrequisiteFilledEvent+= PrequisiteFilled;
        productName = "Expresso";
        produceTime = 2;
    }

    protected override void BeverageReady()
    {
        GWorld.Instance.GetQueue("beverages").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("ExpressoReady", +1);

        if (!productHolder.IsFull)   
        {
            ExpressoMachineConsumer.Reset();
        }
        else
        {
            ProductNumberDecreaseEvent += ExpressoMachineConsumer.Reset;
        }
    }
}
