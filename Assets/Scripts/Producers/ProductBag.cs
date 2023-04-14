using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBag : MonoBehaviour
{
    [SerializeField]
    List<GameObject> productPositions;
    public bool isFull = false;

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


    public void AddProduct(Product newProduct)
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
                    newProduct.transform.SetParent(this.transform);
                    newProduct.transform.localPosition = kvp.Key.transform.localPosition;
                    count++;
                    CheckIfFull();
                    return;
                }
            }
        }
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
}
