using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFinal : Scr_ItemUI
{
    //[SerializeField]
    //public static RawImage test;
    //p static RawImage image;
    // Start is called before the first frame update
    public static Item itemFinal = new Item("Item Final", 0, Item.ItemType.finalTest, 3, 2, Resources.Load<RawImage>("UI/Items/UI_testItem"), 1);

}
