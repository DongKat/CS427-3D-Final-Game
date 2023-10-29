using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
