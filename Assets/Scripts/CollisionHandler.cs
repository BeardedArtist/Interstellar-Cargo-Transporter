using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2.8f;
    
    RocketController rocketController;
    bool isTransitioning = false; // This bool will be used to help set up logic that won't allow for other methods to play if one is activated.
    bool collisionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        rocketController = GetComponent<RocketController>();        
    }

    // Update is called once per frame
    void Update()
    {
        DebugKeysAction();
    }

    void DebugKeysAction()
    {
        // This will help us test the levels a little easier
        // ** TAKE THIS OUT WHEN RELEASING THE GAME **
        if (Input.GetKeyDown(KeyCode.L))
        {
            // LoadNextLevel();
            // This will be our function to quickly load next level (TODO)
            Debug.Log("Load Next Level...");
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // SceneManager.LoadScene("Start Menu");
            // This will load us back into the start menu (TODO)
            Debug.Log("Load Start Menu...");
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        // if isTransitioning is true (when a method is initiated), other methods cannot initiate
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            // "Friendly" will refer to any object that won't damage the player (e.g. starting launch pad)
            case "Friendly":
                break;
            
            case "Refuel Station":
                // TODO: Add refuel system
                break;
            
            // "Finish" refers to the goal
            case "Finish":
                StartGoalSequence();
                break;
            
            // "ReloadLevelWall" refers to the invisible area that will reload player after certain time.
            case "ReloadLevelWall":
                // TODO: add ReloadLevel(); function
                break;
            
            // default refers to the rest of the objects with no tags that will result in damage to the player
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        // disabling the movement script from players. 
        // Invoking a 1 second delay after player collides with object.
        isTransitioning = true;
        
        // TODO: add crash SFX
        // TODO: add crash particle effect

        rocketController.enabled = false;
        Invoke("ReloadLevel", delay);
    }

    private void StartGoalSequence()
    {
        isTransitioning = true;

        //TODO: add win audio
        //TODO: add win particle effects

        rocketController.enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void ReloadLevel()
    {
        // using scene manager to laod scene after player crashes.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        // currentSceneIndex gets current level index.
        // nextLevelIndex adds 1 to the currentSceneIndex to help compare next index. 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;

        // if statement here checks to see if the nextLevelIndex is equal to the total number of levels in the game.
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}
