using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ImageManager : Singleton<ImageManager>
{
    public Sprite GetShopIcon(ShopCategory category, int id)
    {
        Debug.Log(this.shopAtlas);
        return this.shopAtlas.GetSprite(category + "_" + id);
    }
    [SerializeField] private SpriteAtlas shopAtlas;
}
