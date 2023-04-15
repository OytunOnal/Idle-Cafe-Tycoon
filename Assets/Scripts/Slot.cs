using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] GameObject Unlocked;
    [SerializeField] GameObject Open;

    [SerializeField] Slot openNext;
    [SerializeField] SlotState currentState;

    enum SlotState
    {
        Open,
        Close,
        Unlocked
    }

    private void Awake() 
    {
        ChangeSlotState();
    }

    private void ChangeSlotState()
    {
        switch (currentState)
        {
            case SlotState.Close: 
                Unlocked.SetActive(false);
                Open.SetActive(false);
                break;
            case SlotState.Open: 
                Unlocked.SetActive(false);
                Open.SetActive(true);
                break;
            case SlotState.Unlocked: 
                Unlocked.SetActive(true);
                Open.SetActive(false);
                break;
        }
    }

    public void OpenSlot()
    {
        currentState = SlotState.Open;
        ChangeSlotState();
    }

    public void UnlockSlot()
    {
        currentState = SlotState.Unlocked;
        ChangeSlotState();
        openNext?.OpenSlot();
    }
}
