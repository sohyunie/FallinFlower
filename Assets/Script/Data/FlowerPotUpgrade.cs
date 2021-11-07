using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlowerPotUpgrade : MonoBehaviour
{
    public int FlowerPotLevel => this.flowerPotLevel;
    public int EnableNum => this.enableNum;
    public void FlowerData(int flowerPotID, int enableNum)
    {
        this.flowerPotLevel = flowerPotLevel;
        this.enableNum = enableNum;
    }

    public int flowerPotLevel;
    public int enableNum;
}
