using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : Consumer
{
    
    [SerializeField] protected int price;
    // Start is called before the first frame update
    void Start()
    {
        GameObject promtGO = PoolManager.Spawn("Prompt");
        prompt = promtGO.GetComponent<Prompt>();
        promtGO.transform.SetParent(this.transform,true);
        promtGO.transform.localPosition = new Vector3(0,1,0);


        consumableDic.Add(typeof(Money), price);
        currentConsumableDic.Add(typeof(Money),price);
        prompt.AddPromtLine(typeof(Money),
                            PoolManager.Spawn("MoneyPromptLine"),
                            price);
    }

    protected override void Add(Product p)
    {        
        Log.ConsumerLog("Consume");
        int count = --currentConsumableDic[p.GetType()];
        prompt.SetCount(p.GetType(),count);
        if (count == 0) 
        {
            transform.parent.GetComponent<Slot>().UnlockSlot();
        }
    }
}
