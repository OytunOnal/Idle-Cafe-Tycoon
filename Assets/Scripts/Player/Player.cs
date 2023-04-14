using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerBag bag;


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Consumer"))
        {
            for (int i =0; i < bag.products.Count; i++)
                if (other.GetComponent<Consumer>().TakeCollectible(bag.products[i]))
                {
                    bag.RemoveProduct(bag.products[i]);
                    i--;
                }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag.Equals("Producer"))
        {
            if (bag.isFull) return;
                Product p = other.GetComponent<Producer>().GiveCollectible();
                bag.AddProduct(p);
        }
    }


}
