using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : GAgent
{
    new void Start() {
        
        // Call the base start
        base.Start();

    }

    public void GoForACoffee() {
        // Set up the subgoal "isWaiting"
        // And add it to the goals
        SubGoal s1 = new SubGoal("IsWaiting", 1, true);
        goalsDic.Add(s1, 3);

    }


    public void GoHome()
    {
        SubGoal s2 = new SubGoal("IsHome", 1, true);
        goalsDic.Add(s2, 3);
    }
}
