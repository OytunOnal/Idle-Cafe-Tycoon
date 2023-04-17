using UnityEngine;

public class ServeCoffee : GAction 
{

    // Resource in this case = cubicle
    GameObject resource;

    public override bool PrePerform() 
    {              
        // Set our target customer and remove them from the Queue
        target = GWorld.Instance.GetQueue("customers").RemoveResource();
        // Check that we did indeed get a patient
        if (target == null )
        {
            target = this.gameObject;
            // No patient so return false
        }
        return true;
    }

    public override bool PostPerform() 
    {
        GWorld.Instance.GetWorld().ModifyState("NotWaiting", +1);
        resource = inventory.FindItemWithTag("Coffee");
        // Check that we did indeed get a coffee

        if (resource != null )
        {
            target = null;
            // No coffee so return false           
            GWorld.Instance.GetWorld().ModifyState("CanServe", +1);
            return false; 
        } 
        else
        {
            return true;
        }
    }
}
