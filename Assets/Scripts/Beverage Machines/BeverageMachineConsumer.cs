using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeverageMachineConsumer : Consumer
{
    public Action PrequisiteFilledEvent;      

    protected override void Add(Product p)
    {        
        Log.ConsumerLog("Consume");
        int count = --currentConsumableDic[p.GetType()];
        prompt.SetCount(p.GetType(),count);
        if (count == 0) 
        {
            currentConsumableDic.Remove(p.GetType());
            if (currentConsumableDic.Keys.Count == 0) 
            {
                prompt.HidePromt();
                PrequisiteFilledEvent?.Invoke();
            }
        }
    }
}
