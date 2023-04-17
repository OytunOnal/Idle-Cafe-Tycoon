using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductHolder : MonoBehaviour
{
    protected bool isFull = false;
    public bool IsFull {get => isFull; set{}}

    [SerializeField] public int size;

    protected int count;
    protected int Count 
    {
        get => count; 
        set
        {
            count = value;
            isFull = count >= size;
        }
    }

    public abstract bool AddProduct(Product newProduct);
    public abstract Product GetProduct();
    public abstract Product RemoveProduct();
    public abstract void RemoveProduct(Product p);

}

