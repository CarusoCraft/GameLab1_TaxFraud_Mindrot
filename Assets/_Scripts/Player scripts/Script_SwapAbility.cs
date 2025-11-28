using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Script_SwapAbility : MonoBehaviour
{
    [Header("activeBody")] 
    private Rigidbody rb; // Rigidbody of the active body
    private int bodyNumber; // the number of the Active body


    [Header("otherBodies")]
    [SerializeField] private GameObject[] allBodies; // array of all bodies in the scene
    [SerializeField] private GameObject nextBody; // the body to swap to
    [SerializeField] private Vector3 nextPlayerPosition; // the position of the next body you're "swapping" to
    private int n = 1; // nr of body you're swapping to
    private int x = 0; // iterator for allBodies array
    private int amountOfUsedBodies = 0; // counts how many bodies have been used already
    private int totalBodies = 0; // total number of bodies accessed 
    private bool canSwap = false; // checks if swapping is possible
    private bool isGoingNext = false; // checks if the player is cycling to the next body
    private bool outOfBodies = false; // checks if there are no more bodies to swap to

    [Header("Audio")]
    [SerializeField] private AudioSource swapSound; // sound played when swapping bodies
    [SerializeField] private AudioSource noSwapSound; // sound played when no bodies are available to swap to



    private void Start()
    {
        gameObject.tag = "Active"; // sets the player body to active

        x = 0; // Resets iterator for next swap
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component
        allBodies = GameObject.FindGameObjectsWithTag("InActive"); // Finds all inactive bodies in the scene
        totalBodies = allBodies.Length; // sets the total number of bodies
    }

    private void OnEnable()
    {
        x = 0; // Resets iterator for next swap
        rb = GetComponent<Rigidbody>(); // Gets the Rigidbody component
        allBodies = GameObject.FindGameObjectsWithTag("InActive"); // Finds all inactive bodies in the scene
        canSwap = true;
        outOfBodies = false;
        totalBodies += allBodies.Length; // sets the total number of bodies
    }

    private void Update()
    {
        CheckingBodies();

        if (outOfBodies == false)
        {
            if (allBodies.Length > 0)
            {


                while (allBodies[x].GetComponent<playerNumber>().bodyNumber != n) // finds the next body to swap to
                {
                    x++;

                    if (x > allBodies.Length - 1)
                    {
                        x = 0;
                    }
                }

                if (allBodies[x].GetComponent<playerNumber>().bodyNumber == n && allBodies[x].tag != "UsedPlayer") //assigns the next body to swap to
                {
                    nextBody = allBodies[x];
                    nextBody.tag = "NextBody"; // marks the next body to swap to
                    nextPlayerPosition = new Vector3(nextBody.transform.position.x, nextBody.transform.position.y, nextBody.transform.position.z); // stores the position of the next body

                    canSwap = true;
                }
                else
                {
                    canSwap = false;

                    if (isGoingNext == true && n < allBodies.Length)
                    {
                        n++;
                    }
                    else if (isGoingNext == true && n >= allBodies.Length)
                    {
                        n = 1;
                    }
                }

                if (nextBody.tag == "NextBody")
                {
                    nextBody.GetComponent<MeshRenderer>().material.color = Color.purple; // sets the color of the next body to yellowGreen
                }
            }
        }
    }

    // swaps the current body with the selected body
    private void OnSwapBody()
    {

        if (gameObject.tag == "Active") //checks if the current player is in control of an active body
        {

            if(canSwap == true)
            {
                swapSound.Play(); // plays the swap sound

                // "Inhabits" the new body
                gameObject.GetComponent<CharacterController>().enabled = false; //disables the character controller on the current body
                gameObject.GetComponent<PlayerMovement>().enabled = false; //disables the movement script on the current body
                nextBody.GetComponent<BoxCollider>().enabled = false; //disables the collider on the next body
                nextBody.transform.position = new Vector3 (gameObject.transform.position.x , gameObject.transform.position.y , gameObject.transform.position.z); // moves the next body to the current body's position
                gameObject.transform.position = nextPlayerPosition; // moves the current body to the next body's position
                gameObject.GetComponent<CharacterController>().enabled = true; //disables the character controller on the current body
                gameObject.GetComponent<PlayerMovement>().enabled = true; //disables the movement script on the current body
                nextBody.GetComponent<BoxCollider>().enabled = true; //disables the collider on the next body

                nextBody.tag = "UsedPlayer"; //sets the current body to used
                nextBody.GetComponent<MeshRenderer>().material.color = Color.red; // sets the color of the used body to red

                n++; // automatically selects the next body in the array after swapping

                canSwap = false; //resets canSwap

            }

            else
            {
                noSwapSound.Play(); // plays the no swap sound
            }
        }
    }

    private void CheckingBodies()
    {
        if (outOfBodies == false)
        {
            for (int i = 0; i < allBodies.Length; i++)
            {
                if (allBodies[i].tag == "UsedPlayer")
                {
                    amountOfUsedBodies++;
                }
            }
        }
        

        if (amountOfUsedBodies >= totalBodies)
        {
            outOfBodies = true;
        }
        else
        {
            outOfBodies = false;
        }

    }

    
}
