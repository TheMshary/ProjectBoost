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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
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
        if (!isTransitioning)
        {
            audioSource.Stop();
            crashParticles.Play();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(crash, 0.6f);
            Invoke("ReloadLevel", levelLoadDelay);
        }
        isTransitioning = true;
    }
    
    void StartSuccessSequence()
    {
        if (!isTransitioning)
        {
            audioSource.Stop();
            successParticles.Play();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(success, 0.6f);
            Invoke("LoadNextLevel", levelLoadDelay);
        }
        isTransitioning = true;
    }

    void LoadNextLevel()
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
