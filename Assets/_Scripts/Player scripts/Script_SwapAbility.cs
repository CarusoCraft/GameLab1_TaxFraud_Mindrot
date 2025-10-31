using UnityEngine;
using UnityEngine.InputSystem;

public class Script_SwapAbility : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject newBody;
    [SerializeField] private GameObject[] allBodies;
    [SerializeField] private int n = 1; // nr of body you're swapping to
    private int x = 0; // iterator for allBodies array
    private GameObject nextBody;
    private bool canSwap = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component
        allBodies = GameObject.FindGameObjectsWithTag("InActive"); // Finds all inactive bodies in the scene
        n = gameObject.GetComponent<playerNumber>().bodyNumber + 1; // sets n to the next body number
        x = 0; //resets iterator for next swap

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

        if (allBodies.Length > 0)
        { 
            while (allBodies[x].GetComponent<playerNumber>().bodyNumber != n) //looks for the correct body to swap to
            {
                x++;

                if (x > allBodies.Length - 1)
                {
                    x = 0;
                }
            }

            if (allBodies[x].GetComponent<playerNumber>().bodyNumber == n)
            {
                nextBody = allBodies[x];
                canSwap = true;
            }
            else
            {
                canSwap = false;
            }
        }


    }

    private void OnSwapBody()
    {
        if (gameObject.tag == "Active") //checks if the current player is in control of an active body
        {


            if(canSwap == true)
            {

                gameObject.tag = "UsedPlayer"; //sets the current body to used



                // Inhabits the new body
                nextBody.tag = "Active";
                nextBody.AddComponent<PlayerMovement>();
                nextBody.AddComponent<Script_SwapAbility>();
            }

        }

    }


}
