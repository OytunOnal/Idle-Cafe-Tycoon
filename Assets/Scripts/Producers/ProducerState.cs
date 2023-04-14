using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProducerState
{
    public ProducerState nextState;
    protected Producer ownerProducer;
    public string stateName;
    public abstract void PreProcess();
    public abstract void Process();
    public abstract void PostProcess();
}
