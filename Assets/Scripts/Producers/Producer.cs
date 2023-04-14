using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    [SerializeField]
    protected ProductBag productBag;
    ProducerState CurrentState;
    public ProducerState WaitState;
    public ProducerState ProduceState;

    public float productWaitTime;
    protected string productName;

    public bool isBagFull = false;
    public Action ProductNumberDecrease;

    protected void Init() 
    {
        Log.ProducerLog("Initialize");
        WaitState = new ProducerWaitState(this);
        ProduceState = new ProducerProduceState(this);   
        CurrentState = ProduceState;
        CurrentState.PreProcess();
    }

    public Product GiveCollectible()
    {
        Log.ProducerLog("Give Collectible");
        Product p = productBag.RemoveProduct();
        isBagFull = productBag.isFull;

        ProductNumberDecrease?.Invoke();

        return p;
    }

    public void Produce()
    {
        Log.ProducerLog("Produce");
        GameObject newProduct = PoolManager.Spawn(productName);
        if (newProduct == null) return;
        productBag.AddProduct(newProduct.GetComponent<Product>());

        isBagFull = productBag.isFull;
    }

    public void StepState()
    {        
        Log.ProducerLog("StepState");
        CurrentState = CurrentState.nextState;
        CurrentState.PreProcess();
    }
}





