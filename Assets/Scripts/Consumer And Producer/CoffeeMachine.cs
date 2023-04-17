using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    [SerializeField] CoffeeMProducer CoffeeMPro;
    [SerializeField] CoffeeMConsumer CoffeeMCon;

    private void Start() 
    {        
        CoffeeMPro.ConsumePrequisitesEvent += FireConsumeEvent;
        CoffeeMPro.ProductReadyEvent += CoffeeReady;

        CoffeeMCon.PrequisiteFilledEvent+= FirePrequisiteFilled;
    }

    void FirePrequisiteFilled()
    {
         CoffeeMPro.PrequisiteFilledEvent?.Invoke();
    }

    void FireConsumeEvent()
    {
         CoffeeMCon.ConsumeEvent?.Invoke();
    }

    void CoffeeReady()
    {   
        CoffeeMCon.Reset();
        GWorld.Instance.GetQueue("beverages").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("CoffeeReady", +1);
    }
}
