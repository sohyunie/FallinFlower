using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public void SetBaseFlower(bool isBase)
    {
        foreach(Flower flower in flowers)
        {
            flower.SetInVisibleFlower(isBase);
        }
    }
    
    [SerializeField] private List<Flower> flowers;
    [SerializeField] private int index;
}
