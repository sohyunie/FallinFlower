using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UEPopup : MonoBehaviour
{
    public static T GetInstantiateComponent<T>() where T : MonoBehaviour
    {
        string path = StringConst.POPUP_PATH + typeof(T).Name;
        GameObject gameObject = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        T instance = gameObject.GetComponent<T>();
        UEPopup.instance = instance as UEPopup;
        return instance;
    }


    public virtual void ShowPopUp()
    {
        if (this.isShow == true) return;    // 이미 쇼하고 있는 popup이면 다시 쇼 안한다. 
        // 연출
        this.gameObject.SetActive(true);
        this.isShow = true;
    }

    public virtual void DestroyPopUp()
    {
        if (this.isShow == false) return;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        this.isShow = false;
    }

    protected static UEPopup instance;
    private bool isShow = false;
}
