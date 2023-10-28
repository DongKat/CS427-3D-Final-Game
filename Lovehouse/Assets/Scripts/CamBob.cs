using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBob : MonoBehaviour
{
    public Animator anim;
    bool isCrouch = false;
    bool walk;
    // Update is called once per frame
    void Update()
    {
        isCrouch = FindAnyObjectByType<SC_FPSController>().isCrouching;
        if (Input.GetButton("Horizontal") | Input.GetButton("Vertical"))
        {
            walk = true;
            anim.ResetTrigger("idle");
            anim.ResetTrigger("sprint");
            anim.SetTrigger("walk");
            if (walk && Input.GetButton("Sprint") && !isCrouch)
            {
                anim.ResetTrigger("idle");
                anim.ResetTrigger("walk");
                anim.SetTrigger("sprint");
            }
        }
        else
        {
            anim.ResetTrigger("walk");
            anim.ResetTrigger("sprint");
            anim.SetTrigger("idle");
            walk = false;
        }
    }
}
