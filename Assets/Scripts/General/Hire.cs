using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hire : MonoBehaviour
{
    [SerializeField] GameObject waiter;
    [SerializeField] GameObject Panel;
    public void HireStuff()
    {
        if (Wallet.Instance.CoinCount > 50)
        {
            Wallet.Instance.Spend(50);
            waiter.SetActive(true);
            Panel.SetActive(false);
        }
    }
}
