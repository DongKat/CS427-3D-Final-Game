using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitForEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitForEnd() {
        yield return new WaitForSeconds(31);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
