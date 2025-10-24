using UnityEngine;
using UnityEngine.InputSystem;

public class Script_SwapAbility : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject newBody;
    private GameObject[] allBodies;
    [SerializeField] private int n = 1; // nr of body you're swapping to
    private int x = 0; // iterator for allBodies array

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component
        allBodies = GameObject.FindGameObjectsWithTag("InActive"); // Finds all inactive bodies in the scene
        n = gameObject.GetComponent<playerNumber>().bodyNumber + 1; // sets n to the next body number
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
            Debug.Log("n is " + n);

            while (allBodies[x].GetComponent<playerNumber>().bodyNumber != n) //looks for the correct body to swap to
            {
                x++;
                Debug.Log("x is " + x);

            }

            if(allBodies[x].GetComponent<playerNumber>().bodyNumber == n)
            {
                Debug.Log("did the swap");

                gameObject.tag = "UsedPlayer"; //sets the current body to used



                // Inhabits the new body
                allBodies[x].tag = "Active";
                allBodies[x].AddComponent<PlayerMovement>();
                allBodies[x].AddComponent<Script_SwapAbility>();

            }
            else
            {
                Debug.Log("didn't swap");
            }

        }

    }


}
