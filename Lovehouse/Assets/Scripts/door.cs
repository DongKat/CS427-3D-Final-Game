using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject intText, key, lockedText;
    public bool interactable, toggle, isOpen;
    public Animator doorAnim;

    private UnityEngine.AI.NavMeshObstacle obstacle;

    void Start()
    {
        obstacle = GetComponent<UnityEngine.AI.NavMeshObstacle>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            interactable = true;
        }
        if (other.CompareTag("Pumpkin"))
        {
            // Debug.Log("Door opened");
            isOpen = true;
            StartCoroutine("closeDoor");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (key.active == false)
                {
                    toggle = !toggle;
                    if (toggle == true)
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                    else
                    {

                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");
                    }
                    intText.SetActive(false);
                    interactable = false;
                }
                if (key.active == true)
                {
                    lockedText.SetActive(true);
                    StopCoroutine("disableText");
                    StartCoroutine("disableText");
                }
            }
        }

        if (isOpen == true)
        {
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }

    }
    IEnumerator disableText()
    {
        yield return new WaitForSeconds(2);
        lockedText.SetActive(false);
    }

    IEnumerator closeDoor()
    {
        // close door after pumpkin open for 5 secs
        yield return new WaitForSeconds(1);
        obstacle.carving = true;
        yield return new WaitForSeconds(2);
        doorAnim.ResetTrigger("open");
        doorAnim.SetTrigger("close");
        obstacle.carving = false;
        isOpen = false;
    }
}
