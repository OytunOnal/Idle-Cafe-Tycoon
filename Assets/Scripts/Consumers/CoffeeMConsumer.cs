using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMConsumer : Consumer
{
    private int coffeeBeanCount = 3;
    private int  milkCount = 1;

    public Action PrequisiteFilledEvent;
    public Action ConsumeEvent;
    
    private void Start() 
    {
        //Get new prompt from the pool and initialize it
        GameObject promtGO = PoolManager.Spawn("Prompt");
        prompt = promtGO.GetComponent<Prompt>();
        promtGO.transform.SetParent(this.transform,false);
        promtGO.transform.localPosition = new Vector3(0,1,0);


        consumableDic.Add(typeof(CoffeeBean),coffeeBeanCount);
        currentConsumableDic.Add(typeof(CoffeeBean),coffeeBeanCount);
        prompt.AddPromtLine(typeof(CoffeeBean),
                            PoolManager.Spawn("CoffeeBeanPromptLine"),
                            coffeeBeanCount);

        consumableDic.Add(typeof(Milk),milkCount);
        currentConsumableDic.Add(typeof(Milk),milkCount);
        prompt.AddPromtLine(typeof(Milk),
                            PoolManager.Spawn("MilkPromptLine"),
                            milkCount);

        ConsumeEvent += ConsumeAll;
    }

    protected override void Add(Product p)
    {        
        Log.ConsumerLog("Consume");
        productBag.AddProduct(p);
        int count = --currentConsumableDic[p.GetType()];
        prompt.SetCount(p.GetType(),count);
        if (count == 0) 
        {
            currentConsumableDic.Remove(p.GetType());
            if (currentConsumableDic.Keys.Count == 0) 
            {
                prompt.HidePromt();
//                Debug.Log(PrequisiteFilledEvent.GetInvocationList());
                PrequisiteFilledEvent?.Invoke();
            }
        }
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