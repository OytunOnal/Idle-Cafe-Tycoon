using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressoMachineConsumer : BeverageMachineConsumer
{
    private int coffeeBeanCount = 4;
    
    private void Start() 
    {
        //Get new prompt from the pool and initialize it
        GameObject promtGO = PoolManager.Spawn("Prompt");
        prompt = promtGO.GetComponent<Prompt>();
        promtGO.transform.SetParent(this.transform,true);
        promtGO.transform.localPosition = new Vector3(0,1,0);


        consumableDic.Add(typeof(CoffeeBean),coffeeBeanCount);
        currentConsumableDic.Add(typeof(CoffeeBean),coffeeBeanCount);
        prompt.AddPromtLine(typeof(CoffeeBean),
                            PoolManager.Spawn("CoffeeBeanPromptLine"),
                            coffeeBeanCount);
    }

    public void Reset()
    {
        foreach (KeyValuePair<Type, int> kvp in consumableDic)
        {
            // Clone the key
            Type key = kvp.Key;

            // Clone the value
            int value = kvp.Value;

            // Add the cloned key-value pair to the new dictionary
            currentConsumableDic.Add(key, value);
        }
        prompt.SetCount(typeof(CoffeeBean),coffeeBeanCount);
        prompt.gameObject.SetActive(true);
    }
}
