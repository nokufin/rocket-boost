using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip succesSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem succesParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(UnityEngine.Collision other)
    {

        if(!isControllable) { return; };

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("EVERYTHING IS FINE!");
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccesSequence()
    {
        // todo add sfx and particles
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(succesSFX);
        succesParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        // todo add sfx and particles
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}

