using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void healthCheck(int health) //checks the health of the player and if the player should be dead then the player is dead
    {
        if(health == 0 || health < 0)
        {
            SceneManager.LoadScene(2); //ends the game and loads the death screen
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
