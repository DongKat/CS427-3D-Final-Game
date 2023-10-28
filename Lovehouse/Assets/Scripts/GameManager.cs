using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlayerDead = false;

    // Other game-related variables and functions...

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize game state, UI, and other systems.
    }

    static public void PlayerDied()
    {
        instance.isPlayerDead = true;
        // Prompt game over UI.
    }

    // Other functions to manage game state, handle input, etc.
}
