using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_fpsLook : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public float minClamp = -70;
    public float maxClamp = 80;
    public float interactRange = 4f;
    public GameObject inv;

     GameObject DoorUi;
     GameObject GameStateController;
    

    public TextMeshProUGUI tooltip;
    public Image crosshair;
    GameObject character;
    public GameObject interactObject;
    private int pickupStage;
    GameObject pickupObject;
    public Animator wepAni;
    private int aniCount = 1;
    private float aniTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        DoorUi = GetComponentInParent<scr_Player_Controller>().DoorUi;
        GameStateController = GetComponentInParent<scr_Player_Controller>().GameStateController;

        character = this.transform.parent.gameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        if (aniTimer >= 3)
        {
            aniCount = 1;
            aniTimer = 0;
        }
        aniTimer += Time.deltaTime;


        switch (GameStateController.GetComponent<Scr_Ui_Script>().gameState) {
            case Scr_Ui_Script.GameState.Main:
                var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
                smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                mouseLook += smoothV;
                mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

                transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("Holy shoots " + aniCount);
                    //wepAni.SetBool(("mouse1"), true);
                    switch (aniCount) {
                        case 1:
                            wepAni.Play("systemSwing");
                            break;
                        case 2:
                            wepAni.Play("systemSwing2");
                            break;
                        case 3:
                            wepAni.Play("systemSwing3");
                            break;
                        default:
                            aniCount = 1;
                            break;
                    }
                    aniTimer = 0;
                    aniCount += 1;
                    if (aniCount > 3) {
                        aniCount = 1;
                    }

                }
                break;

            
            case Scr_Ui_Script.GameState.DoorKeycode:
                break;
        }


        // item scanning
        RaycastHit scan;
        if (pickupStage == 0)
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out scan, interactRange))
            {
                if (scan.transform.gameObject.CompareTag("Interactable"))
                {
                    interactObject = scan.transform.gameObject;
                    pickupObject = interactObject;
                    //Debug.Log("yeet");
                    string interactName = scan.transform.gameObject.GetComponent<Scr_TestAttribute>().Name;
                    //  vowel check
                    if (interactName[0] == 'a' || interactName[0] == 'A' || interactName[0] == 'e' || interactName[0] == 'E' || interactName[0] == 'i' || interactName[0] == 'I' ||
                        interactName[0] == 'o' || interactName[0] == 'O' || interactName[0] == 'u' || interactName[0] == 'U')
                    {
                        tooltip.text = "An \"" + interactName + "\"";
                    }
                    else
                    {
                        tooltip.text = "A \"" + interactName + "\"";
                    }
                    //tooltip.text = "A " +  ;
                    crosshair.color = Color.green;
                }
            }
            else
            {
                interactObject = null;
                crosshair.color = Color.white;
                tooltip.text = "";
            }
        }
        if (Input.GetButtonDown("Use"))
        {
            if (interactObject == null)
            {
                Debug.Log("invalid");
            }
            else
            {
                switch (interactObject.GetComponent<Scr_TestAttribute>().type) {
                    case Scr_TestAttribute.Type.Enviroment_Pickup_Object:
                        pickupStage += 1;
                        Debug.Log("use " + pickupStage);
                        break;
                    case Scr_TestAttribute.Type.Door:

                        Debug.Log("Gonna cry?");
                        
                        interactObject.GetComponent<scr_door>().OpenAndClose(this.gameObject, interactRange);
                        break;
                    case Scr_TestAttribute.Type.Keypad:
                        DoorUi.SetActive(true);
                        GameStateController.GetComponent<Scr_Ui_Script>().gameState = Scr_Ui_Script.GameState.DoorKeycode;
                        DoorUi.GetComponentInChildren<InputField>().Select();
                        break;
                    case Scr_TestAttribute.Type.Item:
                        Debug.Log("Yeet");
                        inv.gameObject.GetComponent<Scr_Inventory>().PickUpItem(Item.GetItem(interactObject.GetComponent<Item_Component>().itemType));
                        Destroy(interactObject);
                        break;
                    case Scr_TestAttribute.Type.Key:
                        Debug.Log("The key is, fuckoff");
                        Scr_Inventory.keys.Add(interactObject.GetComponent<scr_key>());
                        Destroy(interactObject);
                        break;
                }

            }
            if (pickupStage == 2) {
                Debug.Log("Stop");
                pickupStage = 0;
                pickupObject.GetComponent<Rigidbody>().freezeRotation = false;

            }

        }
        if (Input.GetButtonDown("Fire1") && pickupStage == 1) {
            Debug.Log("Then I started blasting!");
            pickupObject.GetComponent<Rigidbody>().freezeRotation = false;
            pickupObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1500);
            pickupStage = 0;

        }

        if (pickupStage == 1)
        Pickup();
    }

    void Pickup() {

        //Ray pickupPos = new Ray(this.transform.position, this.transform.forward);
        Rigidbody rb = pickupObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        pickupObject.transform.position = this.transform.position + this.transform.forward * 3f;
    }

    public void DropItem(GameObject item) {
        item.transform.position = this.transform.position + this.transform.forward * 3f;
    }



}
