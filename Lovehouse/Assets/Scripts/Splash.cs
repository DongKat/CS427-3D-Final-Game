using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForSplash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForSplash() {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
