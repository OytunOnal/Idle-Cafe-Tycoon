using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour {

    #region Fields

    // Name of the action
    public string actionName = "Action";

    // Cost of the action
    public float cost = 1.0f;

    // Target where the action is going to take place
    public GameObject target;

    // Store the tag
    public string targetTag;

    // Duration the action should take
    public float duration = 0.0f;

    // An array of WorldStates of preconditions
    public WorldState[] preConditions;

    // An array of WorldStates of afterEffects
    public WorldState[] afterEffects;

    // The NavMEshAgent attached to the agent
    public NavMeshAgent agent;

    // Dictionary of preconditions
    public Dictionary<string, int> preConditionsDic;

    // Dictionary of effects
    public Dictionary<string, int> effectsDic;

    // State of the agent
    public WorldStates agentBeliefs;

    // Access our inventory
    public GInventory inventory;

    public WorldStates beliefs;

    // Are we currently performing an action?
    public bool running = false;

    #endregion

    #region Constructor
    public GAction() {

        // Set up the preconditions and effects
        preConditionsDic = new Dictionary<string, int>();
        effectsDic = new Dictionary<string, int>();
    }
    #endregion

    private void Awake() {

        // Get hold of the agents NavMeshAgent
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        // Check if there are any preConditions in the Inspector
        // and add to the dictionary
        if (preConditions != null) {

            foreach (WorldState w in preConditions) {

                // Add each item to our Dictionary
                preConditionsDic.Add(w.key, w.value);
            }
        }

        // Check if there are any afterEffects in the Inspector
        // and add to the dictionary
        if (afterEffects != null) {

            foreach (WorldState w in afterEffects) {

                // Add each item to our Dictionary
                effectsDic.Add(w.key, w.value);
            }
        }
        // Populate our inventory
        inventory = this.GetComponent<GAgent>().inventory;
        // Get our agents beliefs
        beliefs = this.GetComponent<GAgent>().beliefs;
    }

    #region Methods
    public bool IsAchievable() {

        return true;
    }

    /// <summary>
    //check if the action is achievable given the condition of the
    //world and trying to match with the actions preconditions
    /// </summary>
    public bool IsAhievableGiven(Dictionary<string, int> conditions) {

        foreach (KeyValuePair<string, int> p in preConditionsDic) {

            if (!conditions.ContainsKey(p.Key)) {

                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
    #endregion
}
