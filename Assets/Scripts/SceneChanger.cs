using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    private string currentSceneName;
    private VRFadeController transitionManager;

    private void Start()
    {
        transitionManager = VRFadeController.Instance;
        if (transitionManager == null)
        {
            Debug.LogError("SceneTransitionManager not found in the scene!");
        }
    }

    public void ChangeScene(string newSceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(newSceneName));
    }

    public void Initialize(string startScene)
    {
        currentSceneName = startScene;
        SceneManager.LoadScene(startScene, LoadSceneMode.Additive);
        
    }

    private IEnumerator ChangeSceneCoroutine(string newSceneName)
    {
        // Trigger fade out
        transitionManager = VRFadeController.Instance;
        
        if (transitionManager != null)
        {
            transitionManager.FadeOut();
            yield return new WaitForSeconds(transitionManager.FadeDuration);
        }

        // Unload the current scene and load the new one
        if (!string.IsNullOrEmpty(currentSceneName))
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        currentSceneName = newSceneName;
        // Trigger fade in
        if (transitionManager != null)
        {
            transitionManager.FadeIn();
            yield return new WaitForSeconds(transitionManager.FadeDuration);
        }
    }
    
}