using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ItemSlot : MonoBehaviour
{
    public Item item;
    public int count = 0;
    public Vector2[,] slotCords;

    public  ItemSlot(Item item, int count, Vector2[,] slotCords) {
        this.item = item;
        this.count = count;
        this.slotCords = slotCords;
    }
}
