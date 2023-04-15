using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text coinCountText;

    int coinCount = 50;

    public bool isEmpty = false;
    public Money money;
    // Start is called before the first frame update
    

    private void Start() 
    {
        coinCountText.SetText(coinCount.ToString());
    }

    public void Spend()
    {
        coinCount--;
        
        coinCountText.SetText(coinCount.ToString());

        isEmpty = (coinCount<=0);
    }

    public void Earn()
    {
        coinCount++;
        
        coinCountText.SetText(coinCount.ToString());

        isEmpty = false;
    }
}
