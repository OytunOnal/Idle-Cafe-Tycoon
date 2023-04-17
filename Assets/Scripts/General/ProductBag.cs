using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBag : MonoBehaviour
{
    [SerializeField]
    List<GameObject> productPositions;
    private bool isFull = false;

    public bool IsBagFull {get => isFull; set{}}

    Dictionary<GameObject,Product> products = new Dictionary<GameObject, Product>();

    public int size;
    private int count = 0;

    private void Start() 
    {
        foreach (GameObject go in productPositions)
        {
            products.Add(go,null);
        }

        size = productPositions.Count;
    }


    public bool AddProduct(Product newProduct)
    {
        if (!isFull)
        {
            foreach (KeyValuePair<GameObject, Product> kvp in products)
            {
                // Check if the value is null
                if (kvp.Value == null)
                {
                    // Assign a new Product object to the empty space
                    products[kvp.Key] = newProduct;
                    newProduct.transform.SetParent(this.transform,true);
                    newProduct.transform.localPosition = kvp.Key.transform.localPosition;
                    count++;
                    CheckIfFull();
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckIfFull()
    {
        isFull = count >= size ? true : false;
        return isFull;
    }

    public Product RemoveProduct()
    {
        if (products.Count == 0) return null;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            // Check if the value is null
            if (kvp.Value != null)
            {
                // Assign a new Product object to the empty space
                Product p = kvp.Value;
                products[kvp.Key] = null;
                count--;
                CheckIfFull();
                return p;
            }
        }

        return null;
    }

    public Product GetProduct()
    {
        if (products.Count == 0) return null;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            // Check if the value is null
            if (kvp.Value != null)
            {
                return kvp.Value;
            }
        }

        return null;
    }

    public void RemoveProduct(Product p)
    {
        if (products.Count == 0) return ;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            // Check if the value is null
            if (kvp.Value != null && kvp.Value == p)
            {
                products[kvp.Key] = null;
                count--;
                CheckIfFull();
                return ;
            }
        }
    }
}
