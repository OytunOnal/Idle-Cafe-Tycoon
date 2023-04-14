using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour
{
    [SerializeField]    protected ProductBag productBag;
    [SerializeField]    protected GameObject plane;    
    [SerializeField]    protected GameObject activePlane;

    protected List<Type> consumableTypes = new List<Type>();

    public bool TakeCollectible(Product p)
    {
        Log.ConsumerLog("TakeCollectible");
        if (consumableTypes.Contains(p.GetType()))
        {
            Consume(p);
            return true;
        }
        else return false;
    }

    protected virtual void Consume(Product p){}
}
