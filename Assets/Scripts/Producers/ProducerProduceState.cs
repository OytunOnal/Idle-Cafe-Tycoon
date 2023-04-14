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
         Log.ProducerStateLog("ProduceState construct");
        ownerProducer = owner;
        waitTime = ownerProducer.productWaitTime;
        stateName = "Produce State";
        nextState = ownerProducer.WaitState;
    }

    public override void PreProcess()
    {
         Log.ProducerStateLog("ProduceState PreProcess");
        Delay();
    }

    public override void Process()
    {
         Log.ProducerStateLog("ProduceState Process");
        ownerProducer.Produce();
        PostProcess();
    }

    public async void  Delay()
    {
        Log.ProducerStateLog("ProduceState Delay");
        await Task.Delay(1000*(int)waitTime);
        Process();
    }

    public override void PostProcess()
    {
         Log.ProducerStateLog("ProduceState PostProcess");
        ownerProducer.StepState();
    }
}
