using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_TestAttribute : MonoBehaviour
{
    public enum Type
    {
        Enviroment_Pickup_Object,
        Door,
        Item,
        Keypad,
        Key
    };
    public string Name = "David";
    public Type type = Type.Enviroment_Pickup_Object;
    public int health;

    public void subHealth(int sub) {
        health -= sub;
    }
    /* 
    Types:
        0: Enviroment Pickup Object
        1: Door
        2: Item
     */


}
