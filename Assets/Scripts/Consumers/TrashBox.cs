using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrashBox : Consumer
{
    private void Start() 
    {
        consumableDic.Add(typeof(CoffeeBean),int.MaxValue);
        currentConsumableDic.Add(typeof(CoffeeBean),int.MaxValue);
        consumableDic.Add(typeof(Milk),int.MaxValue);
        currentConsumableDic.Add(typeof(Milk),int.MaxValue);
        consumableDic.Add(typeof(Coffee),int.MaxValue);
        currentConsumableDic.Add(typeof(Coffee),int.MaxValue);
    }
    

    protected override void Add(Product trash)
    {        
        Log.ConsumerLog("Consume");
        productHolder.AddProduct(trash);
        DestroyAfter2Seconds(trash);
    }

    private async void DestroyAfter2Seconds(Product trash)
    {        
        Log.ConsumerLog("DestroyAfter2Seconds");
        await Task.Delay(2000);
        Consume(trash);
    }

    protected override void Consume(Product trash)
    {        
        if (trash== null) return;
        productHolder.RemoveProduct(trash);
        base.Consume(trash);
    }
}
