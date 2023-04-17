using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    [SerializeField]    protected GameObject plane;    
    [SerializeField]    protected GameObject activePlane;
   

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Player"))
        {
            activePlane.SetActive(true);
            plane.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) 
    {        
        if (other.tag.Equals("Player"))
        {
            activePlane.SetActive(false);
            plane.SetActive(true);
        }        
    }
}
