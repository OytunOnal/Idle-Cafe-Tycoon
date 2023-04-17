using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] StaticProductHolder productHolder;
    [SerializeField] CoffeeMachineConsumer CoffeeMachineConsumer;

    string productName = "Coffee";
    int produceTime = 2;

    public Action ProductNumberDecreaseEvent;
    private void Start() 
    {        
        CoffeeMachineConsumer.PrequisiteFilledEvent+= PrequisiteFilled;
    }

    void PrequisiteFilled()
    {
        CoffeeMachineConsumer.ConsumeEvent?.Invoke();
        Produce();
    }

    async void Produce()
    {
        await Task.Delay(1000*produceTime);
        GameObject newProduct = PoolManager.Spawn(productName);
        if (newProduct == null) return;
        productHolder.AddProduct(newProduct.GetComponent<Product>());
        CoffeeReady();
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

    void CoffeeReady()
    {   
        GWorld.Instance.GetQueue("beverages").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("CoffeeReady", +1);

        if (!productHolder.IsFull)   
        {
            CoffeeMachineConsumer.Reset();
        }
        else
        {
            ProductNumberDecreaseEvent += CoffeeMachineConsumer.Reset;
        }
    }
}
