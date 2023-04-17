using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    private DynamicProductHolder productHolder;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Consumer"))
        {
            Consumer consumer = other.GetComponent<Consumer>();
            
            for (int i =0; i < productHolder.products.Count; i++)
            {
                Product p = productHolder.products[i];
                if (consumer.TakeCollectible(p))
                {
                    productHolder.RemoveProduct(p);
                    PoolManager.Despawn(p.gameObject);
                    i--;
                }
            }
            productHolder.ReArrangeProducts();
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
        else if (other.tag.Equals("BeverageMachine"))
        {
            if (productHolder.IsFull) return;
                Product p = other.GetComponent<BeverageMachine>().GiveCollectible();
                productHolder.AddProduct(p);
        }
        else if (other.tag.Equals("Purchasable"))
        {
            if (Wallet.Instance.isEmpty) return;

            other.GetComponent<Consumer>().TakeCollectible(Wallet.Instance.money);
            Wallet.Instance.Spend();
        }

        if (other.tag.Equals("Coin"))
        {
            PoolManager.Despawn(other.gameObject);
            
            Wallet.Instance.Earn();
        }
    }


}
