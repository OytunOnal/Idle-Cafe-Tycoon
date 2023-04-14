using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Bag bag;
    
    private void OnTriggerEnter(Collider other) 
    {        
        
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.tag.Equals("Producer"))
        {
            Log.PlayerLog("Producer Trigerr stay");

            for (int i = 0; i< bag.size; i++)
            {
                Product p = other.GetComponent<Producer>().GiveCollectible();
                bag.AddProduct(p); 
            }
        }
    }


}
