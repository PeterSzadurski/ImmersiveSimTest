using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Keypad : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Door;
    public GameObject DoorUi;

    public bool UseKeypad(string inputCode) {
        if (inputCode.Equals(Door.GetComponentInChildren<scr_door>().keycode))
        {
            Debug.Log("Door Unlocked fam!");
            Door.GetComponentInChildren<scr_door>().isLocked = false;
            return true;
        }
        else {
            Debug.Log("You are bad at life");
           return false;
        }
    }

}
