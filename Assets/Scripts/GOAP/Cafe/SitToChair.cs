public class SitToChair : GAction {
    public override bool PrePerform() {
        target = inventory.FindItemWithTag("Chair");

        return true;
    }

    public override bool PostPerform() {

        //beliefs.ModifyState("atCafe", 0);
        transform.LookAt(target.transform.position+ target.transform.forward);

        // Inject waiting state to world states
        GWorld.Instance.GetWorld().ModifyState("Waiting", 1);
        // Patient adds himself to the queue
        GWorld.Instance.GetQueue("customers").AddResource(this.gameObject);
        // Inject a state into the agents beliefs
        //beliefs.ModifyState("atCafe", 1);

        GetComponent<CoffeeConsumer>().OrderCoffee();
        return true;
    }
}
