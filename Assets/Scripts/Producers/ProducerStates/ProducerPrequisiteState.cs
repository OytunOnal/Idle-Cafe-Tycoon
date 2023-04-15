using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProducerPrequisiteState : ProducerState
{
    public delegate void PostProcessDelegate();
    PostProcessDelegate postProcessDelegate;
    public ProducerPrequisiteState(Producer owner)
    {
        ownerProducer = owner;
         Log.ProducerStateLog(ownerProducer.name + " Prequisite construct");
        stateName = "Prequisite State";
        postProcessDelegate = Process;
    }

    public override void PreProcess()
    {
         Log.ProducerStateLog(ownerProducer.name + " Prequisite PreProcess");
         if (ownerProducer.hasPrequisites)
        {
            if (ownerProducer.prequisiteFilled)
            {
                Process();
            }
            else
            {
                if (ownerProducer.PrequisiteFilledEvent == null || !ownerProducer.PrequisiteFilledEvent.GetInvocationList().Contains(postProcessDelegate))
                {
                    ownerProducer.PrequisiteFilledEvent += Process;
                }
            }            
        }
        else
        {
            Process();
        }
    }

    public override void Process()
    {
        Log.ProducerStateLog(ownerProducer.name + " Prequisite Process");
        nextState = ownerProducer.ProduceState;
        PostProcess();
        
    }

    public override void PostProcess()
    {
        if (ownerProducer.PrequisiteFilledEvent != null && ownerProducer.PrequisiteFilledEvent.GetInvocationList().Contains(postProcessDelegate))
        {
            // PostProcess is already subscribed, so remove it from the event
            ownerProducer.PrequisiteFilledEvent -= Process;
        }
        ownerProducer.prequisiteFilled = false;
        ownerProducer.ConsumePrequisitesEvent?.Invoke();
        
         Log.ProducerStateLog(ownerProducer.name + " Prequisite PostProcess");
        ownerProducer.StepState();
    }
}
