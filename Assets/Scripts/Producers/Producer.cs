using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    [SerializeField]    protected StaticProductHolder productHolder;
    
    protected float produceTime;
    protected string productName;

    public float ProduceTime {get => produceTime; set{}}
    public bool IsFull {get => productHolder.IsFull; set{}}

    private ProducerState CurrentState;
    public ProducerState WaitState;
    public ProducerState ProduceState;

    public Action ProductNumberDecreaseEvent;

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
        Product p = productHolder.RemoveProduct();

        if (p!=null) ProductNumberDecreaseEvent?.Invoke();

        return p;
    }

    public void Produce()
    {
        Log.ProducerLog("Produce");
        GameObject newProduct = PoolManager.Spawn(productName);
        if (newProduct == null) return;
        productHolder.AddProduct(newProduct.GetComponent<Product>());
    }

    public void StepState()
    {        
        Log.ProducerLog("StepState");
        CurrentState = CurrentState.nextState;
        CurrentState.PreProcess();
    }
}





