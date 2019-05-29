using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Scr_Inventory : MonoBehaviour
{
    public GameObject ui;
    [SerializeField]
    private GameObject Player;

    private  List<Item> items = new List<Item>();
    //private static List<ItemSlot> itemSlots = new List<ItemSlot>();
    public  bool[,] slots = new bool[,] {
        //{true, true, true, true, true, true},
        //{true, true, true, true, false, false},
       
        
        // {true, true, true, false, false, false},
       // {true, false, false, false, false, false},
        {true, true, false, false, true, false},
        {true, true, false, false, true, false},
        {false, false, false, false, false, false},
        {false, false, false, false, false, false},
        {false, false, false, false, false, false},
        {false, false, false, false, false, false},
        
        //{true, true, true, true, true, false},
        //{true, false, false, false, false, false},
        //{false, false, false, false, false, false},
    };
    public static List<scr_key> keys = new List<scr_key>();

    // public List<ui.GetComponent<Scr_Ui_Script.Item>> inventory = new List<Scr_Ui_Script.Item>();
    // Start is called before the first frame update

    public static bool KeyMatch(GameObject door) {
        foreach (scr_key key in keys) {
            if (key.Door = door) {
                return true;
            }
        }
        return false;
    }

    public  void PickUpItem(Item item) {
        Debug.Log("begin pickup ");
        if (items.Contains(item) && !(items[items.LastIndexOf(item)].stackSize == items[items.LastIndexOf(item)].count))
        {
            items[items.LastIndexOf(item)].count++;
            Debug.Log("Count increased by 1");
            // add to the count thing 
        }

        else   {
            items.Add(item);
       //     foreach (Item i in items) {
                Debug.Log(item.name);
            if (RoomsAvailable(item)) {
                Debug.Log("fax");
                for (int x = 0; x < item.itemSlots.GetLength(0); x++) {
                    for (int y = 0; y < item.itemSlots.GetLength(1); y++)
                    {
                        slots[(int)item.itemSlots[x, y].x, (int)item.itemSlots[x, y].y] = true;
                        Debug.Log("HAHA:   " + item.itemSlots[x, y]);
                    }
                }
                //   Debug.Log("Final test:  "+ item.itemSlots[1, 1]);
             //   Debug.Log("Final final:   " + item.itemSlots[1, 1]);
                RawImage theTest = Instantiate(item.image, ui.gameObject.transform) as RawImage;
                theTest.GetComponent<Scr_ItemUI>().slotCords = item.itemSlots;
                // Debug.Log("The compare cords:  " + item.image.GetComponent<Scr_ItemUI>().slotCords[0, 0]);

                //Debug.Log("Anota test:  " + ui.transform.Find("Slots").gameObject.transform.Find(item.itemSlots[0, 0].x + ","+ item.itemSlots[0, 0].y).gameObject.name);
                theTest.transform.position = ui.transform.Find("Slots").gameObject.transform.Find(item.itemSlots[0, 0].x + "," + item.itemSlots[0, 0].y).gameObject.transform.position;

                Debug.Log("Final finallie 17 ");

                string outputcrap = "";
                for (int r = 0; r < slots.GetLength(0); r++) {

                    for (int c = 0; c < slots.GetLength(1); c++) {
                        if (slots[c, r])
                        {
                          //  Debug.Log("c:" + c +" r:" + r);
                                outputcrap += "  " + slots[c, r];
                        }
                        else {
                            outputcrap += "  " + slots[c, r];
                        }
                    }
                    outputcrap += "\n";
                }
                Debug.Log(outputcrap);
                                
                    //theTest.rectTransform.position = new Vector2(theTest.rectTransform.position.x + (30 * item.itemSlots[0,0].x), theTest.rectTransform.position.y + (30 * item.itemSlots[0, 0].y));
                }
            }

            //Debug.Log(items);
    //    }

    }

    public  List<Item> Items() {
        return items;
    }

    private  bool RoomsAvailable(Item item){
      //  Debug.Log("The official beginning:   " + slots[0,1]);
        int freeX = 0; int freeY = 0;
        Vector2[,] slotCords = new Vector2[item.width, item.height];
        for (int x = 0; x < slots.GetLength(0); x++) {
            int prevY = 0;

            for (int y = 0; y < slots.GetLength(1); y++)
            {
                Debug.Log("that y test:" +y + "  Free Y:" + freeY);
             
                if (freeY == item.height)
                {
                    Debug.Log("Enter the height");
                    // check the x now
                    // backup the y to the begining
                    for (int otherY = freeY - 1; otherY >= 0; otherY--)
                    {
                        //Debug.Log("Another One:  " + otherY + "   plus this    " + y + "  " + x + "    " + freeX);
                        // move through the y

                        //                        Debug.Log("Before the colapse |  x:" + x + "  otherY:" + otherY);
                        int bufferX = x;
                        if (slotCords[0, 0].x == 0)
                        {
                            bufferX = 0;
                        }
                        int counterThing = 0;
                        Debug.Log("Finishing: " + (int)slotCords[item.width - 1, otherY].x);
                        for (int otherX = (int)slotCords[item.width - 1, otherY].x; otherX < item.width; otherX++)
                        {
                            Debug.Log("XD: " +otherX + !slots[(int)otherX + bufferX, (int)slotCords[0, otherY].y]);
                            //Debug.Log("extra:  "  + otherX +" , " +otherY + "   power    "  + slotCords[otherX, otherY] + "  even  " + slotCords[otherX,0]);
                            //Debug.Log("staple  " + slots[otherY, (int)slotCords[otherX, 0].x]);
                            if (!slots[(int)otherX + bufferX, (int)slotCords[0, otherY].y])
                            {
                                //                              Debug.Log("The other x   " + otherX + "   "  + otherY );
                                //    
                                Debug.Log("The buffer:" + bufferX);
                                slotCords[otherX, otherY] = new Vector2(otherX + bufferX, (int)slotCords[0, otherY].y);
                                Debug.Log("the dude  " + slotCords[otherX, otherY] +  "  Over there: " + counterThing);
                                counterThing++;

                                //Debug.Log("The other: other:" +otherX + "   Other Y:  " + otherY);

                            }
                        }
                    }
                    item.itemSlots = slotCords;
                    // item.image.GetComponent<Scr_ItemUI>().slotCords = slotCords;

                    //itemSlots.Add(new ItemSlot(item, 1, slotCords));
                    return true;
                }


                else if (slots[x, y] == false && ( freeY < item.height  ) && 
                    (y <= (6 - item.height) || y - 1 == prevY)// make sure that the height does not exit the inventory  
                    ) // the 2nd and is to make sure that the y does not overflow into another x 
                {
                    Debug.Log("THE Y:" + y);
                    prevY = y;
                    Debug.Log("Freedom " + freeX + "  " + freeY);
                    Debug.Log("The adjuster: " + (5 - (item.height - 1)));
                    if (freeY == item.height)
                    {
                        slotCords[freeX, freeY - 1] = new Vector2(x, y);
                    }
                    else {
                        Debug.Log("pain crash " + freeY);
                        slotCords[freeX, freeY] = new Vector2(x, y);
                        Debug.Log(slotCords[freeX, freeY]);


                    }
                    //    Debug.Log("X log:  " + slotCords[freeX, freeY] + " last free x: " + freeX + "   x:" + x + "   y:" + y);
                    freeY++;
                    Debug.Log("The end " + freeY);
                }
                else if (slots[x, y] == true)
                {
                    Debug.Log("They didn't make it: " + x + " " + y);

                    freeY = 0;

                }


            }

        }
        return false;
    }

    private void UpdateInventory() {

    }

    public bool DropItem(Vector2[,] cord, GameObject item) {
        Player.GetComponentInChildren<scr_fpsLook>().DropItem(item);
       Debug.Log("The compare cords:  " + cord[0,0]);
        foreach (Item i in items)
        {
            if (i.itemSlots == cord)
            {
                Debug.Log("da first count:   " + i.count);
                i.count--;
                Debug.Log("da second count:   " + i.count);

                if (i.count <= 0)
                {
                    Debug.Log("Check 1:  " + slots[2,0]);
                    for (int y = 0; y < i.itemSlots.GetLength(0); y++)
                    {
                        for (int x = 0; x < i.itemSlots.GetLength(1); x++)
                        {
                            slots[(int)i.itemSlots[y, x].x, (int)i.itemSlots[y, x].y] = false;
                            Debug.Log("moscow:   " + i.itemSlots[y, x]);
                        }
                    }
                    Debug.Log("Check 2:  " + slots[2, 0]);


                    i.count = 1;
                    items.Remove(i);

                    return true;
                }
            
            }
        }
        return false;
    }



    public int GetItemByFirstCord(Vector2[,] cord)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemSlots == cord)
            {
                return i;
            }
        }
        return -1 ;
    }

    public bool MoveToNewSlots(Vector2 moveSlots, Vector2[,] cord) {
        if (!slots[(int)moveSlots.x, (int)moveSlots.y])
        {
            Debug.Log("Keep going ");
            Debug.Log("TAKYON:   " + items[GetItemByFirstCord(cord)].name);
            Debug.Log("Sub-Atmoic Penitration   " + items[GetItemByFirstCord(cord)].itemSlots[0, 0]);
            Debug.Log("Rapid fire through your skull  " + slots[(int)cord[0, 0].x, (int)cord[0, 0].y]);
            if (SlotLoop(moveSlots, items[GetItemByFirstCord(cord)]))
            {
                // purge the original slots

                for (int y = 0; y < items[GetItemByFirstCord(cord)].itemSlots.GetLength(0); y++)
                {
                    for (int x = 0; x < items[GetItemByFirstCord(cord)].itemSlots.GetLength(1); x++)
                    {
                        slots[(int)items[GetItemByFirstCord(cord)].itemSlots[y, x].x, (int)items[GetItemByFirstCord(cord)].itemSlots[y, x].y] = false;
                        Debug.Log("moscow:   " + items[GetItemByFirstCord(cord)].itemSlots[y, x]);
                    }
                }

                // the new ones



                for (int moveX = 0; moveX < items[GetItemByFirstCord(cord)].width; moveX++)
                {
                    for (int moveY = 0; moveY < items[GetItemByFirstCord(cord)].height; moveY++)
                    {
                        if (moveX == 0 && moveY == 0)
                        {
                            items[GetItemByFirstCord(cord)].itemSlots[0, 0] = moveSlots;
                            slots[(int)moveSlots.x, (int)moveSlots.y] = true;
                            Debug.Log("new slot = xD" + items[GetItemByFirstCord(cord)].itemSlots[0, 0]);
                        }
                        else {
                            items[GetItemByFirstCord(cord)].itemSlots[moveX, moveY] = new Vector2(moveX + (int)moveSlots.x, moveY + (int)moveSlots.y);
                            Debug.Log("new slot = " + items[GetItemByFirstCord(cord)].itemSlots[moveX, moveY]);
                            slots[moveX + (int)moveSlots.x, moveY + (int)moveSlots.y] = true;

                        }
                    }

                    
                }
                return true;
            }

        }
        return false;
    }

    private bool SlotLoop(Vector2 newSlots, Item item) {
        for (int moveX = 0; moveX < item.width; moveX++) {
            for (int moveY = 0; moveY < item.height; moveY++) {
                if (moveX != 0 && moveY != 0) {
                    // check the new slots
                    if (moveX + newSlots.x > slots.GetLength(0) - 1 || moveY + newSlots.y > slots.GetLength(1) - 1) {
                        return false;
                    }

                    if (slots[moveX + (int)newSlots.x, moveY + (int)newSlots.y]) {
                        Debug.Log("It ain't it cheif");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    void Start()
    {
   //     int i = ui.GetComponent<Scr_Ui_Script>().testKey.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
