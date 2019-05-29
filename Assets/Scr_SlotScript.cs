using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Scr_SlotScript : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public int x = 2;
    public int y = 0;
    public GameObject ui;
    private Vector3 orgPos;
    
    //public Image image;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        if (ui.gameObject.GetComponent<Scr_Inventory>().slots[x,y]) {
            this.GetComponent<RawImage>().color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ui.GetComponent<Scr_Inventory>().slots[x, y])
        {
            this.GetComponent<RawImage>().color = Color.red;
        }
        else {
            this.GetComponent<RawImage>().color = Color.white;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.position = orgPos;
    }

    public void doubleClick(PointerEventData eventData) {


        if (eventData.clickCount > 2) {
            Debug.Log("Stop!");
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        
        Debug.Log("Double click phase " + pointerEventData.clickCount);
        if (pointerEventData.clickCount == 2)
        {
            Debug.Log("double click");
        }
        FindItem();

    }


    public void FindItem() {
        if (ui.GetComponent<Scr_Inventory>().slots[x, y])
        {
            foreach (Item i in ui.GetComponent<Scr_Inventory>().Items())
            {
                for (int otherY = 0; otherY < i.itemSlots.GetLength(0); otherY++)
                {
                    for (int otherX = 0; otherX < i.itemSlots.GetLength(0); otherX++)
                    {
                        if (i.itemSlots[otherX, otherY] == new Vector2(x, y))
                        {
                            Debug.Log("Got em:   " + i.name);
                        }
                    }

                }
            }
        }
        else {
            Debug.Log("Nothing there fam");
        }
    }
}
