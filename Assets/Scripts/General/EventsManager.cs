using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EventsManager 
{
    public static event Action<Vector2> OnMove;
    public static event Action EndMove;

    public static void InvokeOnMove(Vector2 dir)
    {
        OnMove?.Invoke(dir);
    }

    public static void InvokeEndMove()
    {
        EndMove?.Invoke();
    }
}
