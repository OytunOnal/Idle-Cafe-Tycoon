using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeConsumer : Consumer
{

    public Action PrequisiteFilledEvent;
    public Action ConsumeEvent;
    
    private void Start() 
    {
        //Get new prompt from the pool and initialize it
        GameObject promtGO = PoolManager.Spawn("Prompt");
        prompt = promtGO.GetComponent<Prompt>();
        promtGO.transform.SetParent(this.transform,true);
        promtGO.transform.localPosition = new Vector3(0,2,0);


        consumableDic.Add(typeof(Coffee),1);
        currentConsumableDic.Add(typeof(Coffee),0);
        prompt.AddPromtLine(typeof(Coffee),
                            PoolManager.Spawn("CoffeePromptLine"),
                            1);

        prompt.gameObject.SetActive(false);
        ConsumeEvent += ConsumeAll;
    }

    public void OrderCoffee()
    {
        currentConsumableDic[typeof(Coffee)] = 1;
        prompt.gameObject.SetActive(true);
        prompt.SetCount(typeof(Coffee),1);
        
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
                GWorld.Instance.GetWorld().ModifyState("Waiting", -1);
                // Patient adds himself to the queue
                GWorld.Instance.GetQueue("customers").RemoveResource(this.gameObject);
                prompt.HidePromt();
            }
        }
        DestroyAfter5Seconds(p);
    }

    private async void DestroyAfter5Seconds(Product p)
    {        
        Log.ConsumerLog("DestroyAfter5Seconds");
        await Task.Delay(5000);
        if (p == null) return;
        GameObject money = PoolManager.Spawn("Coin");
        money.transform.position = p.transform.position;
        Consume(p);
    }

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
