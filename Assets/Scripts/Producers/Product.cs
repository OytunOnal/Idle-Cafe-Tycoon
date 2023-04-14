using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    public float Height;
    public string productName;

    private MeshRenderer meshRenderer;

    protected void Initialize() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Height = meshRenderer.bounds.size.y;      

    }
    public void Picked(){}
}
