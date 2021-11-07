using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerPotData : MonoBehaviour
{
    public int FlowerPotID => this.flowerPotID;
    public string FlowerPotName => this.flowerPotName;
    public int FlowerPotPrice => this.flowerPotPrice;
    public void FlowerData(int flowerPotID, string flowerPotName, int flowerPotPrice)
    {
        this.flowerPotID = flowerPotID;
        this.flowerPotName = flowerPotName;
        this.flowerPotPrice = flowerPotPrice;
    }

    public int flowerPotID;
    public string flowerPotName;
    public int flowerPotPrice;
}
