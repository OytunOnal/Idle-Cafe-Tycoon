using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal 
{    
    #region Fields

    // Dictionary to store our goals
    public Dictionary<string, int> subGoalsDic;

    // Bool to store if goal should be removed after it has been achieved
    public bool remove;

    #endregion


    #region Constructor
    public SubGoal(string s, int i, bool r) {

        subGoalsDic = new Dictionary<string, int>();
        subGoalsDic.Add(s, i);
        remove = r;
    }

    #endregion
}

public class GAgent : MonoBehaviour 
{
    #region Fields

    // Store our list of actions
    public List<GAction> actions = new List<GAction>();

    // Dictionary of subgoals
    public Dictionary<SubGoal, int> goalsDic = new Dictionary<SubGoal, int>();

    // Our inventory
    public GInventory inventory = new GInventory();

    // Our beliefs
    public WorldStates beliefs = new WorldStates();

    // Access the planner
    GPlanner planner;

    // Action Queue
    Queue<GAction> actionQueue;

    // Our current action
    public GAction currentAction;

    // Our subgoal
    SubGoal currentGoal;

    // Out target destination for the office
    protected Vector3 destination = Vector3.zero;

    #endregion

    public void Start() {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
    }


    bool invoked = false;
    //an invoked method to allow an agent to be performing a task
    //for a set location
    public virtual void CompleteAction() 
    {
        currentAction.agent.SetDestination(transform.position);
        if (currentAction.PostPerform())
        {
            currentAction.running = false;
            invoked = false;
        }
        else
        {            
            actionQueue = null;
            currentAction.running = false;
            currentAction = null;
            invoked = false;
        }
    }

    void LateUpdate() 
    {
        //if there's a current action and it is still running
        if (currentAction != null && currentAction.running) {
            // Find the distance to the target
            float distanceToTarget = Vector3.Distance(destination, this.transform.position);
            // Check the agent has a goal and has reached that goal
            if (distanceToTarget < 1.5f)//currentAction.agent.remainingDistance < 0.5f)
            {
                if (!invoked) {
                    //if the action movement is complete wait
                    //a certain duration for it to be completed
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        // Check we have a planner and an actionQueue
        if (planner == null || actionQueue == null) {
            planner = new GPlanner();

            // Sort the goals in descending order and store them in sortedGoals
            var sortedGoals = from entry in goalsDic orderby entry.Value descending select entry;
            //look through each goal to find one that has an achievable plan
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals) {
                actionQueue = planner.plan(actions, sg.Key.subGoalsDic, beliefs);
                // If actionQueue is not = null then we must have a plan
                if (actionQueue != null) {
                    // Set the current goal
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        // Have we an actionQueue
        if (actionQueue != null && actionQueue.Count == 0) {
            // Check if currentGoal is removable
            if (currentGoal.remove) {
                // Remove it
                goalsDic.Remove(currentGoal);
            }
            // Set planner = null so it will trigger a new one
            planner = null;
        }

        // Do we still have actions
        if (actionQueue != null && actionQueue.Count > 0) {
            // Remove the top action of the queue and put it in currentAction
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform()) {
                // Get our current object
                if (currentAction.targetTag == "")
                {
                    currentAction.running = true;
                    destination = transform.position;
                }
                if (currentAction.target == null && currentAction.targetTag != "")
                    // Activate the current action
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                if (currentAction.target != null) {
                    // Activate the current action
                    currentAction.running = true;
                    // Pass in the office then look for its cube
                    destination = currentAction.target.transform.position;
                    Transform dest = currentAction.target.transform.Find("Destination");
                    // Check we got it
                    if (dest != null)
                        destination = dest.position;

                    // Pass Unities AI the destination for the agent
                    GoToDestination();
                }
            } else {
                // Force a new plan
                actionQueue = null;
            }
        }
    }

    public virtual void GoToDestination()
    {
        currentAction.agent.SetDestination(destination);
    }
}
