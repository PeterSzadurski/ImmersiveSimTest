using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Item
{
    public enum ItemType
    {
        Key, Lockpick, Health_Kit, Test, longSword, finalTest, fists
    };
    //  public GameObjct UI;
    //public int count = 1;
    public string name;
    public int stackSize = 0;
    public ItemType itemType;
    public int width;
    public int height;
    public int count = 1;
    public Vector2[,] itemSlots;
    public RawImage image;
    public int cat;

    /*
     * cat 0: normal consumeable
     *  cat 1: hand equipment
     * 
    
      */


  //  public GameObject go;

    public Item(string name, int stackSize, ItemType itemType, int width, int height, RawImage image, int cat) {
        this.name = name;
        this.stackSize = stackSize;
        this.itemType = itemType;
        this.width = width;
        this.height = height;
        this.image = image;
        this.cat = cat;
       // this.go = go;
    }

    public static Item GetItem(ItemType itemType) {
        switch (itemType) {
             
            case ItemType.longSword:
                Debug.Log("I am the bone of my sword");
                return ItemLongsword.Longsword;
//                  break;
            default:
                return ItemCube.itemCube;
             //   break;
            case ItemType.finalTest:
                return ItemFinal.itemFinal;
            //    break;
            


        }
    }

  //  public UI.get itemType;
    // Start is called before the first frame update

}
