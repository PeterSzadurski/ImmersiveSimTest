using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Ui_Script : MonoBehaviour
{




    public enum GameState {
        Main,
        DoorKeycode,
        Inventory
    };

   // public Item testKey = new Item(1,1,0,ItemType.Key,"TestKey");



    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Almonds:    A C T I V A T E D");
            if (gameState == GameState.Main) {
                Debug.Log("Inventory");
                gameState = GameState.Inventory;

            }
            else if (gameState == GameState.Inventory) {
                Debug.Log("Exit The Inventory");
                gameState = GameState.Main;

            }
        }
    }
}
