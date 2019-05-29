using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Scr_ItemUI1 : MonoBehaviour, IDragHandler, IEndDragHandler,  IDropHandler, IBeginDragHandler
{
    Vector3 orgPos;
    GameObject dropObject;
    public Vector2[,] slotCords;
    //private bool drag = false;
    //RectTransform invPanel = transform as RectTransform;
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        dropObject = Resources.Load<GameObject>("Items/Item");


    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Draggin  " + slotCords[0,0]);

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        //}
        //Debug.Log("Draggin end   " + raycastResults.Count);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        /*if (!drag)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            for (int i = 0; i < raycastResults.Count; i++)
            {
                Debug.Log(i + ".  You owe me your soul |   " + raycastResults[i].gameObject.name);

                //  if (raycastResults[i].gameObject.GetComponent<Scr_Inventory>() != null) {

                //return false;
                //     }
            }
            drag = true;
        }*/

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = orgPos;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        for (int i = 0; i < raycastResults.Count; i++)
        {
            Debug.Log(i + ".  You owe me your soul |   " + raycastResults[i].gameObject.name);

            //  if (raycastResults[i].gameObject.GetComponent<Scr_Inventory>() != null) {

            //return false;
        }
        Debug.Log("and it's time to pay up");
    }

    public void OnDrop(PointerEventData eventData) {
        if (HasMouseLeftInv()) {
            Debug.Log("Item drop:  " + transform.parent.name);
            GameObject droptest = Instantiate(dropObject);
            if (transform.parent.gameObject.GetComponent<Scr_Inventory>().DropItem(slotCords, droptest)) {
                
                Destroy(this.gameObject);
                //Destroy(Instantiate (this.dropObject, transform.position, transform.rotation), 5f);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool HasMouseLeftInv() {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        Debug.Log("Count:  " + raycastResults.Count);
        if (raycastResults.Count == 1)
        {
            return true;
        }
        else {
            for (int i = 0; i <  raycastResults.Count; i++) {
                if (raycastResults[i].gameObject.tag == "Slot") {
                    Vector2 moveCords = new Vector2();
                    Debug.Log("Count dude:  " + raycastResults[i]);
                    string location = raycastResults[i].gameObject.name;
                    string[] cords = location.Split(',');
                    moveCords.x = int.Parse(cords[0]);
                    moveCords.y = int.Parse(cords[1]);
                    Debug.Log("The parse:     " + moveCords);

                    if (transform.parent.gameObject.GetComponent<Scr_Inventory>().MoveToNewSlots(moveCords, slotCords)) {
                        orgPos = raycastResults[i].gameObject.transform.position;
                        this.transform.position = orgPos;
                    }

                }
            }
        }
        //Debug.Log("Count:  " + raycastResults.Count);

        return false; 
    }
}
