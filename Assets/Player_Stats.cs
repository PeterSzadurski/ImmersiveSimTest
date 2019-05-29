using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public Item.ItemType rightHand;
    public GameObject wep;
    // Start is called before the first frame update
    void Start()
    {
        if (rightHand == Item.ItemType.fists) {
            Debug.Log("No item");
            wep.GetComponent<MeshRenderer>().enabled = false;

        }
    }

    void Update() { }

    // Update is called once per frame

}
