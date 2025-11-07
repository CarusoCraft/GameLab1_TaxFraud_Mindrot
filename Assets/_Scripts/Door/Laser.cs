using UnityEngine;

public class Laser : MonoBehaviour
{
    public Restart restart;




    private void Update()
    {
        
    }


    //Calls the function to restart the game
    private void DeathByLaser()
    {
        if (restart != null)
        {
            restart.RestartGame();
        }
    }
}
