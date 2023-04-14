using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProducerWaitState : ProducerState
{
    public delegate void PostProcessDelegate();
    PostProcessDelegate postProcessDelegate;
    public ProducerWaitState(Producer owner)
    {
         Log.ProducerStateLog("WaitState construct");
        ownerProducer = owner;
        stateName = "Wait State";
        postProcessDelegate = PostProcess;
    }

    public override void PreProcess()
    {
         Log.ProducerStateLog("WaitState PreProcess");
         Process();
    }

    public override void Process()
    {
         Log.ProducerStateLog("WaitState Process");
        if (ownerProducer.isBagFull)
        {
            nextState = ownerProducer.WaitState;
            if (ownerProducer.ProductNumberDecrease == null || !ownerProducer.ProductNumberDecrease.GetInvocationList().Contains(postProcessDelegate))
            {
                ownerProducer.ProductNumberDecrease += PostProcess;
            }
        }
        else
        {
            nextState = ownerProducer.ProduceState;
            PostProcess();
        }
    }

    public override void PostProcess()
    {
        if (ownerProducer.ProductNumberDecrease != null && ownerProducer.ProductNumberDecrease.GetInvocationList().Contains(postProcessDelegate))
        {
            // PostProcess is already subscribed, so remove it from the event
            ownerProducer.ProductNumberDecrease -= PostProcess;
        }
         Log.ProducerStateLog("WaitState PostProcess");
        ownerProducer.StepState();
    }
}
