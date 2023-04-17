using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [SerializeField]    protected ProductBag productBag;

    protected Prompt prompt;
    protected Dictionary<Type,int> consumableDic = new Dictionary<Type, int>();
    protected Dictionary<Type,int> currentConsumableDic = new Dictionary<Type, int>();
    
    public bool IsBagFull {get => productBag.IsBagFull; set{}}

    public bool TakeCollectible(Product p)
    {
        Log.ConsumerLog("TakeCollectible");
        if (currentConsumableDic.ContainsKey(p.GetType()) && currentConsumableDic[p.GetType()] > 0)
        {
            Add(p);
            return true;
        }
        else return false;
    }

    protected virtual void Add(Product p){}
    protected virtual void Consume(Product p)
    {
        PoolManager.Despawn(p.gameObject);
    }
}
