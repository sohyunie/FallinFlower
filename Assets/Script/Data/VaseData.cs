using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VaseData : MonoBehaviour
{
    public int VaseID => this.vaseID;
    public string VaseName => this.vaseName;
    public int VasePrice => this.vasePrice;
    public VaseData(int vaseID, string vaseName, int vasePrice)
    {
        this.vaseID = vaseID;
        this.vaseName = vaseName;
        this.vasePrice = vasePrice;
    }

    public int vaseID;
    public string vaseName;
    public int vasePrice;
}
