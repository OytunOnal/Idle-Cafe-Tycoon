using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    #region Singleton Pattern

    private static Wallet _current;

    public static Wallet Instance
    {
         get { return _current ?? (_current = (Wallet) FindObjectOfType(typeof (Wallet))); }
    }

    #endregion

    [SerializeField] TMPro.TMP_Text coinCountText;

    protected int coinCount = 50;
    public int CoinCount 
    {
        get => coinCount; 
        set
        {
            coinCount = value;
            isEmpty = coinCount == 0;
        }
    }

    public bool isEmpty = false;
    public Coin money;
    // Start is called before the first frame update

    

    private void Start() 
    {
        CoinCount = 100;
        coinCountText.SetText(CoinCount.ToString());
    }

    public void Spend()
    {
        CoinCount--;
        
        coinCountText.SetText(CoinCount.ToString());
    }

    public void Spend(int cost)
    {
        CoinCount -= cost;
        
        coinCountText.SetText(CoinCount.ToString());
    }

    public void Earn()
    {
        CoinCount++;        
        coinCountText.SetText(CoinCount.ToString());
    }
}
