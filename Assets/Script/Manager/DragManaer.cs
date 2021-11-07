using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// DragManager 안 쓰는 중

public class DragManaer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("시작 : " + eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // this.transform.position = eventData.position;
        Debug.Log(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        Debug.Log("드래그 되니?");

        // GameUI.Instance.MoveUIPage(UIPageName.InGame);
        // InGameManager.Instance.Init();
    }
}