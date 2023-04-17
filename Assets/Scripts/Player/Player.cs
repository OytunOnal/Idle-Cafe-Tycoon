using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    private DynamicProductHolder productHolder;
    [SerializeField]    private Wallet wallet;


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Consumer"))
        {
            Consumer consumer = other.GetComponent<Consumer>();
            
            for (int i =0; i < productHolder.products.Count; i++)
            {
                
                if (consumer.IsBagFull) return;
                if (consumer.TakeCollectible(productHolder.products[i]))
                {
                    productHolder.RemoveProduct(productHolder.products[i]);
                    i--;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag.Equals("Producer"))
        {
            if (productHolder.IsFull) return;
                Product p = other.GetComponent<Producer>().GiveCollectible();
                productHolder.AddProduct(p);
        }
        else if (other.tag.Equals("CoffeeMachine"))
        {
            if (productHolder.IsFull) return;
                Product p = other.GetComponent<CoffeeMachine>().GiveCollectible();
                productHolder.AddProduct(p);
        }
        else if (other.tag.Equals("Purchasable"))
        {
            if (wallet.isEmpty) return;

            other.GetComponent<Consumer>().TakeCollectible(wallet.money);
            wallet.Spend();
        }

        else if (other.tag.Equals("Coin"))
        {
            PoolManager.Despawn(other.gameObject);
            
            wallet.Earn();
        }
    }


}
