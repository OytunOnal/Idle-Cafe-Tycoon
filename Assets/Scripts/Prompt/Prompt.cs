using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    private Vector3 promptLinePos = Vector3.zero;
    private Vector3 promptLineHeigt = new Vector3(0,0.5f,0);
    public Dictionary<Type,PromptLine> promptLines = new Dictionary<Type, PromptLine>();

    public void AddPromtLine(Type type, GameObject newPromptLineGO, int count)
    {
        PromptLine newPromptLine = newPromptLineGO.GetComponent<PromptLine>();
        newPromptLineGO.transform.SetParent(this.transform,false);
        newPromptLineGO.transform.localPosition = promptLinePos;
        promptLinePos += promptLineHeigt;
        newPromptLine.SetCount(count);
        promptLines.Add(type,newPromptLine);        
    }

    public void SetCount(Type type, int count)
    {
        promptLines[type].SetCount(count);
    }

    public void HidePromt()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowPromt()
    {
        this.gameObject.SetActive(true);
    }

}
