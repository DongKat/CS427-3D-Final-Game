using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
        public GameObject inttext, light;
        public bool toggle = false, interactable;
        public Renderer lightBulb;
        public Material offlight, onlight; 
        public Animator switchAnim;

        void OnTriggerStay(Collider other){
            if (other.CompareTag("MainCamera")){
                inttext.SetActive(true);
                interactable = true;
            }
        }

        void OnTriggerExit(Collider other){
            if (other.CompareTag("MainCamera")){
                inttext.SetActive(false);
                interactable = false;
            }
        }

        void Update(){
            if(interactable == true){
                if(Input.GetKeyDown(KeyCode.E)){
                    toggle = !toggle;
                    switchAnim.ResetTrigger("press"); 
                    switchAnim.SetTrigger( "press");
                    AudioManager.PlayLightSwitch();
                }
            }
            if (toggle == false){
                light.SetActive(false); 
                lightBulb.material = offlight;
            }
            if (toggle == true){
                light.SetActive(true); 
                lightBulb.material = onlight;
            }
        }
}
