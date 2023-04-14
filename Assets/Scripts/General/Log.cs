using UnityEngine;

public static class Log 
{
    
    public static bool isDebugBuild = true;
    public static bool isProducerLogActive = false;
    public static bool isProducerStateLogActive = false;
    public static bool isConsumerLogActive = true;    
    public static bool isPlayerLogActive = true;

    public static void PlayerLog(object message)
    {
        if (isDebugBuild && isPlayerLogActive)
            Debug.Log("Player Log: " + message);
    }

    public static void ConsumerLog(object message)
    {
        if (isDebugBuild && isConsumerLogActive)
            Debug.Log("Consumer Log: " + message);
    }


    public static void ProducerLog(object message)
    {
        if (isDebugBuild && isProducerLogActive)
            Debug.Log("Producer Log: " + message);
    }

    public static void ProducerStateLog(object message)
    {
        if (isDebugBuild && isProducerStateLogActive)
            Debug.Log("ProducerState Log: " + message);
    }

    public static void Initiliaze()
    {
        isDebugBuild = true;
    }
}
