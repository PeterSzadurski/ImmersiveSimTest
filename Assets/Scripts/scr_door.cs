using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_door : MonoBehaviour
{
    private Animator open;
    public string keycode = "0451";
    public bool isLocked = false;
    public int openState = 0;
    public float range = 4f;
    public float originalAngle = 0;
    //public int raduis = 2;
    //public float buffer = 1.5f;
    //public float thickness = 3;
    public float doorTime = 10f;
    private float startTime =0;
    public GameObject GO;
   // float t = 0.0f;
    public bool movement = false;

    // experiments
    private Quaternion startPos;
    private Quaternion rotateOpen;
    private Quaternion rotateClosed;
    private Quaternion rotateTarget;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;
        rotateOpen = transform.rotation * Quaternion.Euler(Vector3.up * 90);
        rotateClosed = transform.rotation * Quaternion.Euler(Vector3.up * -90);
        rotateTarget = rotateOpen;
        //t = Time.time;
    }




    // Update is called once per frame


    void Update()
    {
    
           // doorTime -= 1.0f - Time.deltaTime;
        if (movement)//(doorTime >= 0)
        {
            // Quaternion startRotation = GO.transform.rotation;
            // Quaternion targetRotation = Quaternion.Euler(0, 90, 0) * startRotation;


            startTime += Time.deltaTime;
            if (startTime >= doorTime) {
                startTime = doorTime;
                Debug.Log("Ende");
                movement = false;
            }
            float prec = startTime / doorTime;
           /*switch (openState) {
                case 0:
                    rotateTarget = rotateOpen;
                    Debug.Log("Open 0");
                    break;
                case 1:
                    rotateClosed = transform.rotation * Quaternion.Euler(Vector3.up * -90);
                    rotateTarget = rotateClosed;
                    Debug.Log("Open 1");
                    break;

            }*/ 

            GO.transform.rotation= Quaternion.Lerp(GO.transform.rotation, rotateTarget, prec);
            //Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y +50, transform.position.z), doorTime);

        
            

           // movement = false;

            }
   

    }

    public Vector3 Hi(Vector3 start, Vector3 end, float timeStart, float endTime)
    {
        Debug.Log("Hi!");
        float timeSinceStart = Time.time - timeStart;
        float precentComplete = timeSinceStart / endTime;

        var result = Vector3.Lerp(start, end, precentComplete);
        return result;

    }

    public void StartLerp() {
        startTime = Time.time;
    }

    public void UnlockPassword(string pass) {
        if (pass.Equals(keycode))
        {
            isLocked = false;
        }
        else {
            Debug.Log("Invalid Password");
        }
    }

    public void OpenAndClose(GameObject opener, float openCastRange)
    {
        // startTime = Time.time;
     //   Debug.Log("DETECTED");
        open = GetComponentInParent<Animator>();




        if (!isLocked && openState == 0)
        {

            Vector3 toTarget = (opener.transform.position - transform.position).normalized;

            if (Vector3.Dot(toTarget, transform.forward) > 0)
            {
                Debug.Log("Target is in front of this game object.");
                //   open.Play("Door Open");
                startTime = 0;
                rotateTarget = rotateOpen;
                openState = 1;
                movement = true;

            }
            else
            {
                Debug.Log("Target is not in front of this game object.");
                Debug.Log("idk again ");
                startTime = 0;
                rotateTarget = rotateClosed;
                openState = 2;
                movement = true;
            //    open.Play("Door Close", 0);
            }
            /* if (Physics.BoxCast(new Vector3(GO.transform.position.x, GO.transform.position.y, GO.transform.position.z + otherthing), GO.transform.localScale, GO.transform.forward, out result, GO.transform.rotation, boxthing))
             {
                 Debug.Log("more than one chosen " + result.transform.gameObject.name);

               //  open.Play("Door Close", 0);
             }
             else {
                 Debug.Log("idk ");
                // open.Play("Door Open", 0);


             }*/

            /* if (openCastRange <= range)
             {
                 Debug.Log("more than one chosen again 2" + opener.name);

                 open.Play("Door Open");
                 openState = 1;
             }
             else
             {
                 Debug.Log("idk again ");
                 open.Play("Door Close", 0);
                 openState = 2;

             }
             */
            //isOpen = true;
        }

        else if (!isLocked && openState == 1)
        {
            //     open.Play("Door Open Inv");
            startTime = 0;
            rotateTarget = startPos;
            Debug.Log("Bad idea: " + rotateTarget);
            Debug.Log("Good idea: " + rotateClosed);

            movement = true;
          //  Debug.Log("Open Door inv");
            openState = 0;
            //Debug.Log("Open Door bread");

            //movement = true;

        }
        else if (!isLocked && openState == 2)
        {
            Debug.Log("Close Door yes");
            startTime = 0;
            //   open.Play("Door Close Inv");
            openState = 0;
            rotateTarget = startPos;
            movement = true;
            Debug.Log("Close Door inv");

        }

        if (isLocked) {
            if (Scr_Inventory.KeyMatch(this.gameObject))
            {
                isLocked = false;
                Debug.Log("Unlocked");
            }
            else {
                Debug.Log("not work");

            }
        }

    }
}
