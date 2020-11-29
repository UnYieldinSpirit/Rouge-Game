using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    public Slider healthBar;
    
    public void Update()
    {
        healthBar.value = health;
    }
}
