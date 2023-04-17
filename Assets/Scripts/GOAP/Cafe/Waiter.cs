using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : GAgent
{
    [SerializeField]    protected DynamicProductHolder productHolder;

    protected Dictionary<Type,int> consumableDic = new Dictionary<Type, int>();
    protected Dictionary<Type,int> currentConsumableDic = new Dictionary<Type, int>();

    SubGoal s1 = new SubGoal("CanServe", 1, true);
    SubGoal s3 = new SubGoal("CoffeeServed", 1, false);
    SubGoal s2 = new SubGoal("Waiting", 1, false);


    new void Start() 
    {
        // Set up the subgoal "isWaiting"
        // And add it to the goals
        //goalsDic.Add(s1, 2);        
        goalsDic.Add(s2, 1);
        goalsDic.Add(s3, 3);
        // Call the base start
        base.Start();
        consumableDic.Add(typeof(Coffee),1);
        currentConsumableDic.Add(typeof(Coffee),1);
    }
    

    private void OnTriggerStay(Collider other) 
    {
        if (other.name.Equals("CoffeeMachine"))
        {
            if (productHolder.IsFull) return;
            {
                Product p = other.GetComponent<CoffeeMachine>().GiveCollectible();
                if (p == null) return;
                
                GWorld.Instance.GetWorld().ModifyState("HasCoffee", +1);
                productHolder.AddProduct(p);
                inventory.AddItem(p.gameObject);
            }
        }

        if (other.tag.Equals("Consumer"))
        {
            Consumer consumer = other.GetComponent<Consumer>();
            
            if (productHolder.GetProduct() == null) return;
            if (consumer.transform.position != destination) return;
            if (consumer.IsBagFull) return;
            if (consumer.TakeCollectible(productHolder.GetProduct()))
            {
                inventory.RemoveItem(productHolder.RemoveProduct().gameObject); 
                GWorld.Instance.GetWorld().ModifyState("HasCoffee", -1);           
                    
            }
        }
    }
}
