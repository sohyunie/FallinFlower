using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlant : MonoBehaviour
{
    public bool CanClick => this.canClick;

    public void StartPlanting(float time = 60.0f)
    {
        this.timeText.gameObject.SetActive(true);
        this.time = time;
        this.maxTime = time;
        this.canClick = false;
    }
    
    [SerializeField] private TextMeshPro timeText;
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obj3;
    private float time = 0.0f;
    private float maxTime = 0.0f;
    private bool canClick = false;

    protected void Update()
    {
        // if(this.time > 0.0f)
        // {
        //     this.time -= Time.deltaTime;
        //     int min = Mathf.FloorToInt(this.time / 60.0f);
        //     float sec = this.time % 60.0f;
        //     this.timeText.text = min.ToString("n0") + ":" + sec.ToString("n0");
        //     obj1.SetActive(this.time > (this.maxTime * 0.66f));
        //     obj2.SetActive((this.maxTime * 0.33f) < this.time && this.time < (this.maxTime * 0.66f));
        //     obj3.SetActive((this.maxTime * 0.33f) > this.time);
        //     this.canClick = false;
        // } 
        // else
        // {
        //     this.timeText.text = "DONE!";
        //     this.canClick = true;
        // }
    }
}
