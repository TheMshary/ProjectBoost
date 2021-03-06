using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning | collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with a friendly object.");
                break;
            case "Finish":
                Debug.Log("Collided with the finish pad.");
                //next level
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Collided with a random object. Gun die nw. bie.");
                //die
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stop existing sfx
        crashParticles.Play(); // particles
        GetComponent<Movement>().enabled = false; // disable movement
        audioSource.PlayOneShot(crash, 0.6f); // sfx
        Invoke("ReloadLevel", levelLoadDelay); // reload level
    }
    
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stop existing sfx
        successParticles.Play(); // particles
        GetComponent<Movement>().enabled = false; // disable movement
        audioSource.PlayOneShot(success, 0.6f); // sfx
        Invoke("LoadNextLevel", levelLoadDelay); // load next level
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // restart game and start over at the first level/scene
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // buildIndex gets us the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
