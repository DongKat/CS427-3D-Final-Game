using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupkey : MonoBehaviour
{
    public GameObject inttext, key, announce;
    public bool interactable;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inttext.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inttext.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if(interactable ==true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inttext.SetActive(false);
                interactable = false;

                AudioManager.PlayPickup();
                
                GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(MakeTextActive());
            }
        }
    }
    IEnumerator MakeTextActive()
    {
        announce.SetActive(true);
        yield return new WaitForSeconds(2);
        announce.SetActive(false);
        key.SetActive(false);
    }
}
