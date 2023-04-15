using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    public float Height;
    public string productName;

    [SerializeField]
    private MeshRenderer meshRenderer;

    protected void Initialize() 
    {
        Height = meshRenderer.bounds.size.y;      

    }
    public void Picked(){}
}
