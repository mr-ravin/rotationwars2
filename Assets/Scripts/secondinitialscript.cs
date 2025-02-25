using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class secondinitialscript : MonoBehaviour
{
    private string sceneNameToLoad="PlayModeScene";
    private string mainSceneToLoad="FirstScene";
    private float wait_time = 3.0f;
    void Awake()
    {
        #if UNITY_ANDROID || UNITY_STANDALONE_LINUX
          Application.targetFrameRate = 60;
          QualitySettings.vSyncCount = 1;
          Time.fixedDeltaTime = 1f / 60f;
        #elif UNITY_WEBGL
          Time.fixedDeltaTime = 1f / 60f; 
        #endif
    }

    void Start()
    {
        Invoke(nameof(GotoNextScene), wait_time);
    }

    void Update()
    {
      #if !UNITY_ANDROID      
        #if ENABLE_INPUT_SYSTEM
          if (Keyboard.current.escapeKey.wasPressedThisFrame)
          {
              GoToMainScreen();
          }
        #else
          if (Input.GetKeyDown(KeyCode.Escape))
          {
              GoToMainScreen();
          }
        #endif
      #endif
    }
    
    public void GoToMainScreen()
    {
      SceneManager.LoadScene(mainSceneToLoad);
    }
    public void GotoNextScene()
    {
      SceneManager.LoadScene(sceneNameToLoad);
    }
}