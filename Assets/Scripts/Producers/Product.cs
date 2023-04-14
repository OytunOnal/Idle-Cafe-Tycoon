using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    public float Height;

    private MeshRenderer meshRenderer;

    private void Start() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Height = meshRenderer.bounds.size.y;      

    }
    public void Picked(){}
}
