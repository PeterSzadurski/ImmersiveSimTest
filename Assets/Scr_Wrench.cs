using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Wrench : MonoBehaviour
{
    public Collider wrenchCol;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Interactable" && other.gameObject.GetComponent<Scr_TestAttribute>().type == Scr_TestAttribute.Type.Door 
            && ani.GetCurrentAnimatorStateInfo(0).IsName("systemSwing") || ani.GetCurrentAnimatorStateInfo(0).IsName("systemSwing2") || ani.GetCurrentAnimatorStateInfo(0).IsName("systemSwing3") && other.gameObject.GetComponent<Scr_TestAttribute>() != null) {
            Debug.Log("Wrong house");
            other.gameObject.gameObject.GetComponent<Scr_TestAttribute>().subHealth(1);
            if (other.gameObject.gameObject.GetComponent<Scr_TestAttribute>().health <= 0) {
                Debug.Log("door broke");
                Destroy(other.gameObject.transform.parent.transform.parent.gameObject);
            }
        }
    }

}
