using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticProductHolder : ProductHolder
{   
    [SerializeField] List<GameObject> productPositions;
    Dictionary<GameObject,Product> products = new Dictionary<GameObject, Product>();

    private void Start() 
    {
        foreach (GameObject go in productPositions)
        {
            products.Add(go,null);
        }

        Count = 0;
    }

    public override bool AddProduct(Product newProduct)
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
                    Count++;
                    return true;
                }
            }
        }
        return false;
    }

    public override Product GetProduct()
    {
        if (products.Count == 0) return null;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            // Check if the value is null else return the first product
            if (kvp.Value != null)
            {
                return kvp.Value;
            }
        }
        // return null if empty
        return null;
    }
    
    public override Product RemoveProduct()
    {
        if (products.Count == 0) return null;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            // Check if the value is null else remove and return the first product
            if (kvp.Value != null)
            {
                Product p = kvp.Value;
                products[kvp.Key] = null;
                Count--;
                return p;
            }
        }
        // return null if empty
        return null;
    }


    // Remove given Product
    public override void RemoveProduct(Product p)
    {
        if (products.Count == 0) return ;

        foreach (KeyValuePair<GameObject, Product> kvp in products)
        {
            if (kvp.Value != null && kvp.Value == p)
            {
                products[kvp.Key] = null;
                Count--;
                return ;
            }
        }
    }
}
