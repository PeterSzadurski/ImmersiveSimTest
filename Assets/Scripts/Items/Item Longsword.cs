using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLongsword : Scr_ItemUI
{
    public static Item Longsword = new Item("Longsword", 1, Item.ItemType.longSword, 1, 3, Resources.Load<RawImage>("UI/Items/UI_longword"), 1);


}
