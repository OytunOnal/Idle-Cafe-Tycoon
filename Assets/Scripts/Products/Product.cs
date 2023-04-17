using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    public Vector3 pSize;
    public string productName;

    [SerializeField]
    private MeshRenderer meshRenderer;

    protected void Initialize() 
    {
        pSize = meshRenderer.bounds.size; 
    }
    public void Picked(){}
}
