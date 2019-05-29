using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCube : Scr_ItemUI
{
    //[SerializeField]
    //public static RawImage test;
    //p static RawImage image;
    // Start is called before the first frame update
    public static Item itemCube = new Item("Item Cube", 0, Item.ItemType.Test, 2, 2, Resources.Load<RawImage>("UI/Items/UI_testItem"), 0);

}
