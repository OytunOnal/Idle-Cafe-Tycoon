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
        ownerProducer = owner;
         Log.ProducerStateLog(ownerProducer.name + " WaitState construct");
        stateName = "Wait State";
        postProcessDelegate = PostProcess;
    }

    public override void PreProcess()
    {
         Log.ProducerStateLog(ownerProducer.name + " WaitState PreProcess");
         Process();
    }

    public override void Process()
    {
         Log.ProducerStateLog(ownerProducer.name + " WaitState Process");
        if (ownerProducer.IsBagFull)
        {
            nextState = ownerProducer.WaitState;
            if (ownerProducer.ProductNumberDecreaseEvent == null || !ownerProducer.ProductNumberDecreaseEvent.GetInvocationList().Contains(postProcessDelegate))
            {
                ownerProducer.ProductNumberDecreaseEvent += PostProcess;
            }
        }
        else
        {
            nextState = ownerProducer.PrequisiteState;
            PostProcess();
        }
    }

    public override void PostProcess()
    {
        if (ownerProducer.ProductNumberDecreaseEvent != null && ownerProducer.ProductNumberDecreaseEvent.GetInvocationList().Contains(postProcessDelegate))
        {
            // PostProcess is already subscribed, so remove it from the event
            ownerProducer.ProductNumberDecreaseEvent -= PostProcess;
        }
         Log.ProducerStateLog(ownerProducer.name + " WaitState PostProcess");
        ownerProducer.StepState();
    }
}
