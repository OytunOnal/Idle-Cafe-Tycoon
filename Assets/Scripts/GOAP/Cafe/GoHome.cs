using UnityEngine;

public class GoHome : GAction {
    
    GameObject resource;
    public override bool PrePerform() 
    {
        resource = inventory.FindItemWithTag("Chair");
        GWorld.Instance.GetQueue("chairs").AddResource(resource);
        GWorld.Instance.GetWorld().ModifyState("FreeChair", +1);
        inventory.RemoveItem(resource);
        return true;
    }

    public override bool PostPerform() {

        PoolManager.Despawn(this.gameObject);
        return true;
    }
}
