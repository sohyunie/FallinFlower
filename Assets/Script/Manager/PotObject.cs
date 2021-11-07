using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotObject : MonoBehaviour
{
    public int FlowerCont => this.flowers.Count;
    public void CheckFlower(int layer)
    {
        List<Flower> checkList = this.flowers.FindAll(f => f.Layer >= layer + 1);
        foreach(Flower flower in checkList)
        {
            flower.CheckFlowerLayer(this.flowers.FindAll(f => f.Layer < flower.Layer));
        }
    }

    public void RemoveFlower(Flower flower)
    {
        this.flowers.Remove(flower);
    }

    public void SetPot(Transform spawnTransform)
    {
        this.transform.parent = spawnTransform; 
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        this.transform.localScale = Vector3.one;
    }

    // 꽃잎을 다 떼면 새로 생겨나게 하자.
    public void SetStartTransfrom(GameObject flower)
    {
        flower.transform.parent = this.startTransform;
        flower.layer = 0;  
        flower.transform.localPosition = initVector3;  
        flower.transform.localRotation = Quaternion.identity; // 짐벌락
    }

    protected void Update()
    {
        // 꽃잎 속도 빠르게
        if (this.timeElapsed > 0)
        {
            this.timeElapsed -= Time.deltaTime;
        }
        else
        {
            this.timeElapsed = 1.0f;
            if (this.animator != null)
            {
                this.animator.speed += Time.deltaTime;
            }
        }
    }

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform potTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Flower> flowers = new List<Flower>();
    private Vector3 initVector3;
    private float timeElapsed = 0.0f;
}
