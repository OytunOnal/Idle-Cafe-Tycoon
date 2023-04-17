using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : MonoBehaviour
{
    [SerializeField]
    protected StaticProductHolder productHolder;
    
    protected float produceTime;
    protected string productName;
    public bool hasPrequisites = false;

    public float ProductWaitTime {get => produceTime; set{}}
    public bool IsFull {get => productHolder.IsFull; set{}}

    private ProducerState CurrentState;
    public ProducerState WaitState;
    public ProducerState ProduceState;

    public ProducerState PrequisiteState;

    public Action ProductNumberDecreaseEvent;
    public Action PrequisiteFilledEvent;
    public Action ConsumePrequisitesEvent;

    public Action ProductReadyEvent;

    public bool prequisiteFilled = false;

    protected void Init() 
    {
        PrequisiteFilledEvent += PrequisiteFilled;
        Log.ProducerLog("Initialize");
        WaitState = new ProducerWaitState(this);
        ProduceState = new ProducerProduceState(this); 
        PrequisiteState = new ProducerPrequisiteState(this);   
        CurrentState = PrequisiteState;
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
        ProductReadyEvent?.Invoke();
    }

    public void StepState()
    {        
        Log.ProducerLog("StepState");
        CurrentState = CurrentState.nextState;
        CurrentState.PreProcess();
    }

    public void PrequisiteFilled()
    {      
        prequisiteFilled = true;
    }
}





