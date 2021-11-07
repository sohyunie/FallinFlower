using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour 
{
    public bool IsRemoveAction => this.isRemoveAction;
    public bool IsBaseFlower => this.isInvisibleFlower;
    public int Layer => this.layer;

    public void CheckFlowerLayer(List<Flower> flowers)
    {
        bool isInVisible = false;
        Debug.Log("flowerCount : " + flowers.Count);
        foreach(Flower flower in flowers)
        {
            float distance = Vector2.Distance(this.transform.position, flower.transform.position);
            Debug.Log(distance);
            if(Mathf.Abs(distance) < diff)
            {
                isInVisible = true;
                break;
            }
        }
        this.SetInVisibleFlower(isInVisible);
    }

    public void SetInVisibleFlower(bool isInVisible)
    {
        this.isInvisibleFlower = isInVisible;
        Color color = Color.white;  
        if(this.isInvisibleFlower)
        {
            color.r = 0.5f;
            color.g = 0.5f;
            color.b = 0.5f;
        }
        this.renderer.material.SetColor("_BaseColor", color);
    }

    public void RemoveAction(Vector3 initPos, Vector3 afterPos)
    {
        
        SoundManager.Instance.PlaySFXSound(SFX.swipePetal);
        initPos.z = this.transform.position.z; 
        afterPos.z = this.transform.position.z; 

        Vector3 dir = (afterPos - initPos).normalized * 10;
        dir.z = 0; 
        Vector3 targetPos = this.transform.position + dir;  
        this.maximumLength = (this.transform.position - targetPos).magnitude; 

        Debug.Log("RemoveAction");
        this.transform.parent = InGameManager.Instance.OrthoTransform;
        this.targetPos = targetPos;
        this.isRemoveAction = true; 
    }

    public void RemoveByTime(float time = 2.0f)
    {
        this.StartCoroutine(this.RemoveFlower(time));
    }

    void Start()
    {
    }

    void Update()
    { 
        if(isRemoveAction)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, Time.deltaTime * 4.0f);
            Color color = Color.white;  
            color.a = (this.transform.position - this.targetPos).magnitude / this.maximumLength;
            this.renderer.material.SetColor("_BaseColor", color);
            if(color.a < 0.1f)
            {
                if(this.gameObject != null)
                {
                    Debug.Log("destroy : " + this.name);
                    this.isRemoveAction = false;   
                    Destroy(this.gameObject);
                }
            }
            return;
        }
    }

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private int layer;
    [SerializeField] private bool isInvisibleFlower;
    [SerializeField] private float diff;
    private Vector3 targetPos;
    private float maximumLength;
    private bool isRemoveAction;

    private IEnumerator RemoveFlower(float time)
    {
        while(time > 0)
        {
            time -= Time.deltaTime;
            Color color = Color.white;  
            color.a = time;
            this.renderer.material.SetColor("_BaseColor", color);
            Debug.Log(color.a);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}