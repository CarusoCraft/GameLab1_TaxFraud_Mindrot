using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Script : MonoBehaviour
{

    public GameObject DeathMenu;
    void Start()
    {
        DeathMenu.SetActive(false);
    }
    public void Death()
    {
        DeathMenu.SetActive(true);
    }

}