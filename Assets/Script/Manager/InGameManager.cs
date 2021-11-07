using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    public Transform OrthoTransform => this.orthoTransform;
    public bool IsKeepGoing => this.isKeepGoing;
    public int Stage => this.stage; 
    public int Heart => this.heart;

    public void ForceDie()
    {
        Debug.Log("Die");
        // GameUI.Instance.CloseWindow();

        this.DeleteFlower();
        GameUI.Instance.Refresh();
    }

    public string GetLeftFlower()   
    {
        return this.flowerCount.ToString();
    }
    
    public void AddRemoveFlower()
    {
        if(this.stage <= 0)
            return;

        this.flowerCount++;
        GameUI.Instance.Refresh();
        if(this.currentFlower.FlowerCont == 0)
        {   
            if(UnityEngine.Random.Range(0.0f, 1.0f) < 0.2f)
                UnityAdsHelper.Instance.ShowRewardedAd();
            this.NextStage();
        }
    }

    // 목숨 세팅
    public void SetHeart(int heart)
    {
        this.heart = heart;
        GameUI.Instance.Refresh();
    }

    public void InitGame(bool isKeepGoing = false)
    { 
        this.isKeepGoing = isKeepGoing;
        this.SetHeart(NumberConst.DEFAULT_HEART);
        GameUI.Instance.InitGameUI();
        if(!isKeepGoing)
        {
            this.flowerCount = 0;
            this.stage = 0;
            this.NextStage();
        }
    }

    public void DeleteFlower()
    {
        if(this.currentVase != null) GameObject.Destroy(this.currentVase.gameObject);
        if(this.currentFlower != null) GameObject.Destroy(this.currentFlower.gameObject);
    }


    protected void Update()
    {
        if(this.stage <= 0)
            return;
        // (1) 터치를 했는지 여부 판단
        // isTouched는 2가지를 의미
        // 1. isTouched true일때는 마우스가 눌려져있는 상태야.
        // 2. 여러 개의 터치는 안되게 할거야. 첫번 째 터치만 인식.
        if(!isTouched && Input.GetMouseButtonDown(0) && this.target == null)
        {
            isTouched = true;
            this.target = GetClickedObject();
            if(this.target != null)
            {
                Debug.Log(this.target);
                if(this.target.name.Equals("petal"))
                {
                    if(this.target.transform.parent.GetComponent<Flower>().IsBaseFlower)
                    {
                        this.target = null;
                        this.MissAction();
                    }
                } 
                else
                {
                    this.target = null;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            this.isTouched = false; 
        }

        if(this.isTouched) 
        {
            if(this.target != null && this.initPos.Equals(Vector3.zero))
            // target이 있는 상태, initpos가 세팅되지 않은 상태
            {
                if(!(this.currentFlower.FlowerCont == 0))
                {
                   Vector3 diff = new Vector3(1080/2, 1920/2, 0);
                    Vector3 worldMousePos = Input.mousePosition - diff;
                    this.initPos = worldMousePos;
                    Debug.Log("Touch start : " + this.initPos);
                } 
            }
        }
        else
        {
            // 터치 끝 좌표 계산
            if(this.target != null)
            {
                Debug.Log("Touch end");

                Vector3 diff = new Vector3(1080/2, 1920/2, 0);
                Vector3 worldMousePos = Input.mousePosition - diff;
                Vector3 afterPos = worldMousePos; 

                Flower flower = this.target.transform.parent.GetComponent<Flower>();   
                if(flower != null && !flower.IsRemoveAction)
                {
                    flower.RemoveAction(this.initPos, afterPos);
                    this.currentFlower.RemoveFlower(flower);
                    this.currentFlower.CheckFlower(flower.Layer);
                }
                if(this.target.name.Equals("petal"))
                {
                    this.AddRemoveFlower();
                }

                target = null;
                this.initPos = Vector3.zero;
            }
        }
    }

    [SerializeField] private Transform orthoTransform;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private NewPlant newPlant;

    private bool isTouched;
    private GameObject target;
    private Vector3 initPos = Vector3.zero;

    private int heart;
    private PotObject currentFlower;
    private GameObject currentVase;
    private int flowerCount = 0;
    private int stage = 0;
    private bool isKeepGoing = false;

    // 새로 Pot을 생성하는 코드
    private void NextStage()
    {
        //flowerCount = 0;   
        this.stage++;
        GameUI.Instance.Refresh(); 

        if(this.currentFlower != null)
            Destroy(this.currentFlower.gameObject);
        if(this.currentVase != null)
            Destroy(this.currentVase.gameObject);

        var child = this.orthoTransform.GetComponentsInChildren<Transform>();
        foreach(Transform obj in child)
        {
            if(obj.gameObject.name == "PetalTransform")
                continue;
            Destroy(obj.gameObject);
        }
        // RenewalCode
        // 꽃 세팅
        int flowerId = DataManager.Instance.UserData.currentFlower;
        if(this.stage > 1)
        {
            List<PurchaseData> flowerList = DataManager.Instance.UserData.itemList.FindAll(i => i.category == ShopCategory.Flower);
            string randomFlowerName = flowerList[UnityEngine.Random.Range(0, flowerList.Count)].name;
            Debug.Log("count : " + flowerList.Count);
            PurchaseItemData selectFlower = DataManager.Instance.FindItemMetaDataByName(randomFlowerName);
            flowerId = selectFlower.ID;
        }
        this.currentFlower = GameObject.Instantiate(Resources.Load(StringConst.FLOWER_PATH + "Flower_" + flowerId) as GameObject, Vector3.zero, Quaternion.identity).GetComponent<PotObject>();    
        this.currentFlower.gameObject.SetActive(true); 
        this.currentFlower.SetPot(this.spawnTransform);  // ---> 꽃화분이 생성되는 위치 Transform
        this.currentFlower.CheckFlower(0);

        // 화병 세팅
        this.currentVase = GameObject.Instantiate(Resources.Load(StringConst.VASE_PATH + "Vase_" + DataManager.Instance.UserData.currentVase) as GameObject, Vector3.zero, Quaternion.identity) as GameObject;    
        this.currentVase.transform.parent = this.spawnTransform; 
        this.currentVase.transform.localPosition = Vector3.zero;
        this.currentVase.transform.localRotation = Quaternion.identity;
        this.currentVase.transform.localScale = Vector3.one;
    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target; 
    }

    private void MissAction()
    {
        Handheld.Vibrate();
        this.heart--;
        if(this.heart == 0)
        {
            Handheld.Vibrate();
            if(UnityEngine.Random.Range(0.0f, 1.0f) < 0.33f)
                UnityAdsHelper.Instance.ShowRewardedAd();
            Debug.Log("Die");
            GameOverPop.ShowPop(this.flowerCount);
            // SoundManager.Instance.StopBGMSound();
            // GameUI.Instance.CloseWindow();
        }
        GameUI.Instance.Refresh();
    }
}