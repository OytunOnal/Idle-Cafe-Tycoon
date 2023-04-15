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
}
