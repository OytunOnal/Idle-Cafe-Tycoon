using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
    private int testCount = 500000;
    private GameObject go;
    System.Random randomNo = new System.Random();

    int[] intarr= new int[10];

    private void Test1()
    {
        for (int i = 0; i < testCount; i++) 
        {
            UnityEngine.Random.Range(0, 100);         
        }
    }

    private void Test2()
    {
        for (int i = 0; i < testCount; i++)
        {
        }
    }

    private void Test3()
    {
        for (int i = 0; i < testCount; i++)
        {
            randomNo.Next(1, 5);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test1();
            Test2();
            Test3();
        }
    }
}
