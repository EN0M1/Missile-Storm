using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public void GoToGame()
    {
        StartCoroutine(WaitForSoundAndTransition("GamePlay"));
    }

    public void GoToMenu()
    {
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void GoToPlaneSelection()
    {
        StartCoroutine(WaitForSoundAndTransition("PlaneSelection"));
    }

    public void GoToTutorial()
    {
        StartCoroutine(WaitForSoundAndTransition("HowToPlay"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
