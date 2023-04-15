using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProducerProduceState : ProducerState
{
    private float waitTime;

    public ProducerProduceState(Producer owner)
    {
        ownerProducer = owner;
         Log.ProducerStateLog(ownerProducer.name + " ProduceState construct");
        waitTime = ownerProducer.ProductWaitTime;
        stateName = "Produce State";
        nextState = ownerProducer.WaitState;
    }

    public override void PreProcess()
    {
         Log.ProducerStateLog(ownerProducer.name + " ProduceState PreProcess");
        Delay();
    }

    public override void Process()
    {
         Log.ProducerStateLog(ownerProducer.name + " ProduceState Process");
        ownerProducer.Produce();
        PostProcess();
    }

    public async void  Delay()
    {
        Log.ProducerStateLog(ownerProducer.name + " ProduceState Delay");
        await Task.Delay(1000*(int)waitTime);
        Process();
    }

    public override void PostProcess()
    {
         Log.ProducerStateLog(ownerProducer.name + " ProduceState PostProcess");
        ownerProducer.StepState();
    }
}
