using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private int MainMenuScene;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject par_PauseMenu;
    [SerializeField] private GameObject par_PauseMenuMainScreen;
    [SerializeField] private GameObject par_PauseMenuInstructionsScreen;
    [SerializeField] private Player_Stats PlayerStatsScript;

    //public but hidden variables
    //script variables
    [HideInInspector] public bool isGamePaused;
    //player variables
    [HideInInspector] public int money;
    [HideInInspector] public Vector3 rocketStartPadPos;
    [HideInInspector] public List<GameObject> rocketUpgrades = new List<GameObject>();
    //level variables
    [HideInInspector] public List<GameObject> disabledGameobjects = new List<GameObject>();

    private void Awake()
    {
        par_PauseMenuMainScreen.SetActive(false);
        par_PauseMenuInstructionsScreen.SetActive(false);
        par_PauseMenu.SetActive(false);

        //initial data update
        GetPlayerStats();
    }
    private void Update()
    {
        if (!isGamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }
        else if (isGamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }

    //updates all player stats so theyre loaded correctly when the level is restarted
    public void GetPlayerStats()
    {
        //clears all old list data from previous level
        rocketUpgrades.Clear();
        disabledGameobjects.Clear();

        //updates player stat variables
        rocketStartPadPos = rocket.transform.localPosition;
        money = PlayerStatsScript.money;
        //health
        //fuel

        Debug.Log("Successfully updated player level stats.");
    }
    public void OpenPauseMenu()
    {
        isGamePaused = true;

        Time.timeScale = 0;

        par_PauseMenu.SetActive(true);
        par_PauseMenuMainScreen.SetActive(true);
        par_PauseMenuInstructionsScreen.SetActive(false);

        Debug.Log("Game is paused!");
    }
    public void ReturnToGame()
    {
        par_PauseMenuMainScreen.SetActive(false);
        par_PauseMenuInstructionsScreen.SetActive(false);
        par_PauseMenu.SetActive(false);

        Time.timeScale = 1;

        isGamePaused = false;

        Debug.Log("Game is no longer paused!");
    }
    public void RestartCurrentLevel()
    {
        // ### IMPORTANT START ###
        //THIS FUNCTION NEEDS TO KNOW THE BEGINNING STATES OF ALL OF THE PLAYER AND LEVEL VARIABLES
        //DO NOT DELETE ANY GAMEOBJECTS IN THE LEVEL, INSTEAD USE GameObject.SetActive(false);
        //AND ADD ALL DISABLED GAMEOBJECTS TO disabledGameobjects LIST

        //RUN GetPlayerStats(); AT THE START OF EVERY NEW LEVEL TO UPDATE PLAYERS STATS
        //IN THAT LEVEL SO RESTARTING CURRENT LEVEL WILL GO SMOOTHLY
        // ### IMPORTANT END ###

        //moves rocket back to current level start position
        rocket.transform.localPosition = rocketStartPadPos;
        rocket.transform.eulerAngles = Vector3.zero;
        rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rocket.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //resets player other variables
        //reset health
        //reset fuel
        //reset money
        PlayerStatsScript.money = money;

        //enables all disabled gameobjects
        foreach (GameObject disabledGameobject in disabledGameobjects)
        {
            disabledGameobject.SetActive(true);
        }
        disabledGameobjects.Clear();
        //remove all rocket upgrades here

        rocketUpgrades.Clear();

        par_PauseMenuMainScreen.SetActive(false);
        par_PauseMenuInstructionsScreen.SetActive(false);
        par_PauseMenu.SetActive(false);

        isGamePaused = false;

        Time.timeScale = 1;
    }
    public void ShowInstructions()
    {
        par_PauseMenuMainScreen.SetActive(false);
        par_PauseMenuInstructionsScreen.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}