using System.Collections.Generic;
using UnityEngine;

public class GetCoffee : GAction {

    // Resource in this case = cubicle
    GameObject resource;
    private Dictionary<string, int> worldStates;

    public override bool PrePerform() 
    {        
        if    (inventory.FindItemWithTag("Coffee") == null) 
        {   
            return true;
        }
        else
            return false;
    }

    public override bool PostPerform() 
    {      
        GWorld.Instance.GetWorld().ModifyState("NotWaiting", +1);
        resource = inventory.FindItemWithTag("Coffee");
        // Check that we did indeed get a coffee
        if (resource == null)
        {
            // No coffee so return false           
            return false; 
        } 
        else
        {
            return true;
        }
    }
}
