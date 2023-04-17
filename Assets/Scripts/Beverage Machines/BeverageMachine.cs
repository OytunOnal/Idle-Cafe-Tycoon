using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BeverageMachine : MonoBehaviour
{
    [SerializeField] protected StaticProductHolder productHolder; 

    protected string productName;
    protected int produceTime;

    public Action ProductNumberDecreaseEvent;

    protected void PrequisiteFilled()
    {
        Produce();
    }

    async void Produce()
    {
        await Task.Delay(1000*produceTime);
        GameObject newProduct = PoolManager.Spawn(productName);
        if (newProduct == null) return;
        productHolder.AddProduct(newProduct.GetComponent<Product>());
        BeverageReady();
    }

    public Product GiveCollectible()
    {
        Log.ProducerLog("Give Collectible");
        Product p = productHolder.RemoveProduct();

        if (p!=null) 
        {
            ProductNumberDecreaseEvent?.Invoke();
            ProductNumberDecreaseEvent = null;
        }

        return p;
    }

    protected abstract void BeverageReady();
}
