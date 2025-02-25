using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class initialscript : MonoBehaviour
{   
    private string sceneNameToLoad="SecondScene";
    public Button PlayButton; 
    public Button ExitButton;
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
        PlayButton = GameObject.Find("ButtonPlay").GetComponent<Button>();
        PlayButton.onClick.AddListener(StartGame);
        ExitButton = GameObject.Find("ButtonExit").GetComponent<Button>();
        ExitButton.onClick.AddListener(ExitGame);
    }

    #if !UNITY_ANDROID
    void Update()
    { 
        #if ENABLE_INPUT_SYSTEM
          if (Keyboard.current.spaceKey.wasPressedThisFrame)
          {
              StartGame();
          }
          if (Keyboard.current.escapeKey.wasPressedThisFrame)
          {
              ExitGame();
          }
        #else
          if (Input.GetKeyDown(KeyCode.Space))
          {
              StartGame();
          }
          if (Input.GetKeyDown(KeyCode.Escape))
          {
              ExitGame();
          }
        #endif
    }
    #endif
    
    public void StartGame()
    {
      SceneManager.LoadScene(sceneNameToLoad);
    }
    public void ExitGame()
    {
      Application.Quit();
    }
}