using UnityEngine;
using UnityEngine.InputSystem;

public class Script_SwapAbility : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject newBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component
    }

    private void Update()
    {
        if (gameObject.tag == "Active")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green; // sets the color of the active body to green
        }

        if (gameObject.tag == "UsedPlayer")
        {
            // Disables movement and ability scripts on used bodies
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gameObject.GetComponent<Script_SwapAbility>().enabled = false;

            gameObject.GetComponent<MeshRenderer>().material.color = Color.red; // sets the color of the used body to red

        }
    }

    private void OnSwapBody()
    {
        if (gameObject.tag == "Active") //checks if the current player is in control of an active body
        {

            if (GameObject.FindWithTag("InActive") == true) //checks if there is an inactive body to swap to
            {
                newBody = GameObject.FindWithTag("InActive"); // Finding the next body to inhabit
                gameObject.tag = "UsedPlayer"; //sets the current body to used


                // Inhabits the new body
                newBody.tag = "Active";
                newBody.AddComponent<Script_SwapAbility>();
                newBody.AddComponent<PlayerMovement>();

                
            }
           


        }

    }


}
