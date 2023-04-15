using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptLine : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text productCountTxt;

    public void SetCount(int count)
    {
        productCountTxt.SetText(count.ToString());
    }
}
