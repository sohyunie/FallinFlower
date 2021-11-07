using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerData : MonoBehaviour
{
    public int FlowerID => this.flowerID;
    public string FlowerName => this.flowerName;
    public int FlowerPrice => this.flowerPrice;
    public FlowerData(int flowerID, string flowerName, int flowerPrice)
    {
        this.flowerID = flowerID;
        this.flowerName = flowerName;
        this.flowerPrice = flowerPrice;
    }

    public int flowerID;
    public string flowerName;
    public int flowerPrice;
}
