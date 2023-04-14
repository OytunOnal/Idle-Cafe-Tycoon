using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeTree : Producer
{
    //public List<CoffeeBean> Beans;
    // Start is called before the first frame update
    void Start()
    {
     //   foreach (CoffeeBean bean in Beans)
      //      productList.AddProduct(bean);

        productName = "CoffeeBean";
        productWaitTime = 3;

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
