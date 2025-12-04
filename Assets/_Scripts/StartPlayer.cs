using UnityEngine;

public class StartPlayer : MonoBehaviour
{

    public GameObject MovementTutorial;
    public GameObject ButtonTutorial;
    public GameObject SwapTutorial;
    public GameObject GameplayPanel;
    public GameObject PanelButton;


    private float startCooldown = 4;
    private float buttonCooldown = 1;
    private float swapCooldown = 3;

    private bool panelClosed = true;
    private bool startPlay = false;

    private void Start()
    {
        
    }


    private void FixedUpdate()
    {

        if (startCooldown > 0 && panelClosed == false)
        {
            startCooldown -= Time.deltaTime;
            
        }
        else if (startCooldown <= 0)
        {
            startCooldown = 0;
            startPlay = true;
            Debug.Log("Start to play.");
            MoveTutorial();
            buttonCooldown -= Time.deltaTime;
        }

        if (buttonCooldown > 0 && startCooldown == 0)
        {
            buttonCooldown -= Time.deltaTime;
        }
        else if (buttonCooldown < 0)
        {
            buttonCooldown = 0;
            ButtonTutorial.SetActive(true);
        }

        if ( buttonCooldown == 0 && swapCooldown > 0)
        {
            swapCooldown -= Time.deltaTime;
        }
        else if(swapCooldown < 0)
        {
            swapCooldown = 0;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && swapCooldown == 0)
        {
            ButtonTutorial.SetActive(false);
            SwapTutorial.SetActive(true );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SwapTutorial.SetActive(false );
        }
    }


    private void MoveTutorial()
    {
        MovementTutorial.SetActive(false);
    }


    public void GameplayExplained()
    {
        panelClosed = false;
        GameplayPanel.SetActive(false);
    }


}
