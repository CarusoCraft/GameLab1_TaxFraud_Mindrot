using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Script : MonoBehaviour
{

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject playerControlls;

    private void Start()
    {
        deathMenu.SetActive(false);
    }
    public void Death()
    {
        Debug.Log("you died");
        deathMenu.SetActive(true);
        playerControlls.GetComponent<PlayerMovement>().enabled = false;
    }

}