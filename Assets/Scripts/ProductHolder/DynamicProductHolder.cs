using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicProductHolder  : ProductHolder
{
    public List<Product> products = new List<Product>();
    private Vector3 lastPosition = Vector3.zero;
    [SerializeField] bool isVertical = false;

    public override bool AddProduct(Product newProduct)
    {
        if (newProduct == null) return false;
        if (products.Contains(newProduct)) return false;
        if (!isFull)
        {
            products.Add(newProduct);
            ArrangePosition(newProduct);
            Count++;
            return true;
        }
        return false;
    }

    public override Product GetProduct()
    {
        int pCount = products.Count;
        if (pCount == 0 ) return null;
        return products[products.Count-1]; 
    }

    public override Product RemoveProduct()
    {
        int pCount = products.Count;
        if (pCount == 0 ) return null;
        Product p = products[products.Count-1];     
        float offset = isVertical ? lastPosition.y - p.pSize.y : lastPosition.x - p.pSize.x;
        lastPosition = new Vector3 (0,offset,0);
        products.RemoveAt(pCount-1);
        Count--;
        return p;
    }

    public override void RemoveProduct(Product p)
    {
        products.Remove(p);
        Count--;
    }

    public void ReArrangeProducts()
    {
        lastPosition = Vector3.zero;

        foreach (Product p in products)
        {
            ArrangePosition(p);
        }
    }

    private void ArrangePosition(Product p)
    {
        p.transform.SetParent(this.transform,true);
        p.transform.localPosition = lastPosition;
        
        float offset = isVertical ? lastPosition.y + p.pSize.y : lastPosition.x + p.pSize.x;
        lastPosition = new Vector3 (0,offset,0);
    }
}
