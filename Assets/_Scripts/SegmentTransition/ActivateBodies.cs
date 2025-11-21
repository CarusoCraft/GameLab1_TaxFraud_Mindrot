using UnityEngine;

public class ActivateBodies : MonoBehaviour
{
    [SerializeField] private GameObject[] bodiesToActivate; // array of bodies to activate


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (bodiesToActivate.Length > 0)
        {
            Debug.Log("did this");
            for (int i = 0; i < bodiesToActivate.Length; i++)
            {
                Debug.Log("enabled bodies");
                bodiesToActivate[i].SetActive(true); // activates all bodies in the array
            }

            // refreshes the list of bodies in the swap ability script
            other.GetComponent<Script_SwapAbility>().enabled = false; 
            other.GetComponent<Script_SwapAbility>().enabled = true;

        }
        
    }   
    
}
