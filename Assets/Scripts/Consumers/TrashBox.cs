using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrashBox : Consumer
{
    private void Start() 
    {
        consumableTypes.Add(typeof(CoffeeBean));
        consumableTypes.Add(typeof(Milk));
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Player"))
        {
            activePlane.SetActive(true);
            plane.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) 
    {        
        if (other.tag.Equals("Player"))
        {
            activePlane.SetActive(false);
            plane.SetActive(true);
        }        
    }

    protected override void Consume(Product trash)
    {
        
        Log.ConsumerLog("Consume");
        productBag.AddProduct(trash);
        DestroyAfter2Seconds(trash);
    }

    private async void DestroyAfter2Seconds(Product trash)
    {
        
        Log.ConsumerLog("DestroyAfter2Seconds");
        await Task.Delay(2000);
        productBag.RemoveProduct(trash);
        PoolManager.Despawn(trash.gameObject);
    }
}
