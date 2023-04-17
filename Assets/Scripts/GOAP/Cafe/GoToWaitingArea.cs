public class GoToWaitingArea : GAction {
    public override bool PrePerform() {

        return true;
    }

    public override bool PostPerform() 
    {
        GWorld.Instance.GetWorld().SetState("NotWaiting", 0);
        return true;
    }
}
