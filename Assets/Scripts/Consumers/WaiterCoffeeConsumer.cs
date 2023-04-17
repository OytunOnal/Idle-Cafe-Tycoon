using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaiterCoffeeConsumer : Consumer
{

    public Action PrequisiteFilledEvent;
    public Action ConsumeEvent;
    
    

    protected override void Consume(Product p)
    {        
        if (p== null) return;
        productBag.RemoveProduct(p);
        base.Consume(p);

        GetComponent<Customer>().GoHome();
    }

    protected  void ConsumeAll()
    {        
        Product p = productBag.RemoveProduct();

        while (p!= null)
        {
            Consume(p);
            p = productBag.RemoveProduct();
        }

        foreach (KeyValuePair<Type, int> kvp in consumableDic)
        {
            // Clone the key
            Type key = kvp.Key;

            // Clone the value
            int value = kvp.Value;

            // Add the cloned key-value pair to the new dictionary
            currentConsumableDic.Add(key, value);
        }
    }
}
