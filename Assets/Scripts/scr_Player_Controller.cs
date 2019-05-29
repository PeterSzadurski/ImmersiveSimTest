using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Player_Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10f;
    public Rigidbody rb;
    public GameObject debug;
    public float fuckoff;
    public GameObject DoorUi;
    public GameObject GameStateController;
    private Animator anime;
    private Transform rArm;
    private Quaternion why;
    private bool firstly;
    private Vector3 soul;
    float straffe;
    float translation;
   // public Collider charCollide;
   //public Collider wep;
   // public Collider floor;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        firstly = false;
        // hide useless ui
        DoorUi.SetActive(false);
        //  hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        anime = GetComponentInChildren<Animator>();
        // anime.GetFloat("translation");
        //Debug.Log("translation  " + anime.GetFloat("translation"));
        //rArm = anime.GetBoneTransform(HumanBodyBones.RightShoulder);
        //   why = rArm.rotation * Quaternion.Euler(new Vector3(180,0,0));
        // rArm.Rotate(0,0,90);
        // why = rArm.rotation;
        //anime.get

     //   Physics.IgnoreCollision(charCollide, wep);
       // Physics.IgnoreCollision(floor, wep);

    }

    // Update is called once per frame
    void Update()
    {

        translation = Input.GetAxis("Vertical") * speed;
      //  Debug.Log("Translation value: " + Input.GetAxisRaw("Vertical"));
        straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        anime.SetInteger("translation", (int)Input.GetAxisRaw("Vertical"));
        straffe *= Time.deltaTime;


        transform.Translate(straffe, 0, translation);

        if (translation > 0) /* player is moving forward */
        {
            // set animation to walk forward
        }

        //  forward and backward movement
        switch (GameStateController.GetComponent<Scr_Ui_Script>().gameState) {
            case Scr_Ui_Script.GameState.Main:


                if (Input.GetButtonDown("Jump"))
                {
                    rb.AddForce(this.transform.up * jumpForce);
                }
                if (Input.GetKeyDown("e"))
                {
                    Debug.Log("E!");

                }


                break;
            case Scr_Ui_Script.GameState.DoorKeycode:
                Debug.Log("Door mode");
                if (Input.GetButtonDown("Submit")) {
                    if (GetComponentInChildren<scr_fpsLook>().interactObject.GetComponent<Scr_Keypad>().UseKeypad(DoorUi.GetComponentInChildren<InputField>().text))
                    {
                        GameStateController.GetComponent<Scr_Ui_Script>().gameState = Scr_Ui_Script.GameState.Main;
                        DoorUi.GetComponentInChildren<InputField>().text = null;
                        DoorUi.SetActive(false);
                    }
                    else {
                        DoorUi.GetComponentInChildren<InputField>().text = null;
                        DoorUi.GetComponentInChildren<InputField>().Select();
                    }
                }
                break;

            case Scr_Ui_Script.GameState.Inventory:

                break;

                // debug.GetComponent<scr_door>().movement = true;
                //   debug.transform.Rotate(0,90,0);
                //  Debug.Log("Euler! " + debug.transform.eulerAngles.y);



        }


        if (Input.GetKeyDown("escape"))
        {
            DoorUi.SetActive(false);
            // Debug.Log("Unlocked");
            Cursor.lockState = CursorLockMode.None;
            GameStateController.GetComponent<Scr_Ui_Script>().gameState = Scr_Ui_Script.GameState.Main;


        }
        //Debug.Log("It went through");

    }
    void LateUpdate(){
        if (!firstly)
        {
           // rArm.rotation = why;
       //     firstly = true;
        }

        else
        {
          //  soul = rArm.transform.eulerAngles;
          //  Debug.Log("The king of limbo");
         //   rArm.transform.LookAt(cam.transform);
            //rArm.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
        }
        //rArm.rotation  = new  Quaternion( rArm.rotation.x, rArm.rotation.y, rArm.transform.rotation.z, cam.transform.rotation.w);
        //  rArm.rotation = Quaternion.Inverse (cam.transform.rotation);
    }

}


