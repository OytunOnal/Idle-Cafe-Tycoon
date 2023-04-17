using UnityEngine;

public class GoToCafe : GAction {
    GameObject resource;
    public override bool PrePerform() {

        resource = GWorld.Instance.GetQueue("chairs").RemoveResource();
        GWorld.Instance.GetWorld().ModifyState("FreeChair", -1);
        inventory.AddItem(resource);
        return true;
    }

    public override bool PostPerform() {

        return true;
    }
}
