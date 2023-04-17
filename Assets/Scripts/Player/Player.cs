using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    private PlayerBag bag;
    [SerializeField]    private Wallet wallet;


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Consumer"))
        {
            Consumer consumer = other.GetComponent<Consumer>();
            
            for (int i =0; i < bag.products.Count; i++)
            {
                
                if (consumer.IsBagFull) return;
                if (consumer.TakeCollectible(bag.products[i]))
                {
                    bag.RemoveProduct(bag.products[i]);
                    i--;
                }
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
