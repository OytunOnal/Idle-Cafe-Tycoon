using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Dictionary<string, int> worldStates= new Dictionary<string, int>();
    // Start is called before the first  frame update
    void Start()
    {
        worldStates = GWorld.Instance.GetWorld().GetStates();
        CheckTableStatus();
    }

    private async void CheckTableStatus()
    {        
        if (worldStates.ContainsKey("FreeChair") && worldStates["FreeChair"] > 0) 
        {
            GameObject newCustomer = PoolManager.Spawn("Customer");
            if (newCustomer == null) return;
            newCustomer.transform.position = this.transform.position;
            newCustomer.GetComponent<Customer>().GoForACoffee();
        }
        await Task.Delay(Random.Range(2,10)*1000);
        CheckTableStatus();
    }
}
