using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerScore = 0;
    public int playerLives = 3;

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

    // Other functions to manage game state, handle input, etc.
}
