using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GWorld.Instance.GetQueue("chairs").AddResource(this.gameObject);
        GWorld.Instance.GetWorld().ModifyState("FreeChair", +1);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
