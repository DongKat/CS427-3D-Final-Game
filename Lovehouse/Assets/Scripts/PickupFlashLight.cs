using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlashLight : MonoBehaviour
{
    public GameObject fl_intText, fl_table, fl_hand;
    public bool fl_interactable;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            fl_intText.SetActive(true);
            fl_interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            fl_intText.SetActive(false);
            fl_interactable = false;
        }
    }
    void Update()
    {
        if (fl_interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fl_intText.SetActive(false);
                fl_interactable = false;

                AudioManager.PlayPickup();
                
                fl_hand.SetActive(true);
                fl_table.SetActive(false);
            }
        }
    }
}
