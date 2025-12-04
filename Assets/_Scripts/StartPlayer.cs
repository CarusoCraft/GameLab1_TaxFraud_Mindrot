using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    public GameObject Player_Active;
    public GameObject MovementTutorial;

    private float startCooldown = 2f;

    private bool startPlay = false;

    private void Awake()
    {
        MovementTutorial.SetActive(true);
    }

    private void Start()
    {
        Instantiate(Player_Active.transform); //Needs to spawn at -10, 8, -20
        Player_Active.SetActive(true);
    }


    private void FixedUpdate()
    {

        if (startPlay == false)
        startCooldown -= Time.deltaTime;


        if(startCooldown == 0)
        {
            startPlay = true;
        }


        
    }


    private void InstatiatePlayer()
    {
        Player_Active.SetActive(true);

    }

}
