using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class platformscript : MonoBehaviour
{
    private string mainSceneToLoad="FirstScene";
    private Canvas canvas;
    private TextMeshProUGUI textMeshProWin;
    private TextMeshProUGUI textMeshProDraw;
    private TextMeshProUGUI textMeshProShrink;
    private TextMeshProUGUI textMeshProStart;
    private float speed = 80.0f, delta_time;
    private float speed_rate = 0.5f, max_speed = 250.0f;
    private float shrink_rate = 0.04f; // How fast the platform shrinks
    private float min_scale = 0.15f; 
    private float new_x, new_z, old_y = 1.0f;
    private float shrink_wait = 3.0f, shrink_counter, elapsed_time, shrink_start_time = 10f;
    private bool not_is_shrink_text_shown = true, shrink_to_be_initiated = false;
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
        canvas = GameObject.Find("GameTriggers")?.transform.Find("Canvas")?.GetComponent<Canvas>();
        textMeshProWin = canvas.transform.Find("TextGameStatusWin")?.GetComponent<TextMeshProUGUI>();
        textMeshProDraw = canvas.transform.Find("TextGameStatusDraw")?.GetComponent<TextMeshProUGUI>();
        textMeshProShrink = canvas.transform.Find("TextGameStatusShrink")?.GetComponent<TextMeshProUGUI>();
        textMeshProStart = canvas.transform.Find("TextGameStatusStart")?.GetComponent<TextMeshProUGUI>();
        elapsed_time = 0.0f; 
        shrink_to_be_initiated = false;
    }

    void Update()
    {   
        delta_time = Time.deltaTime;
        elapsed_time += delta_time; // Increment elapsed time
        if(speed < max_speed)
        {
            speed = Mathf.Min(speed + speed_rate, max_speed);
        }
        if (!shrink_to_be_initiated && elapsed_time >= shrink_start_time)
        {
            shrink_counter = 0.0f; 
            shrink_to_be_initiated = true; // Start shrinking after 30 seconds
        }
        if(shrink_to_be_initiated)
        {   // Displaying text on canvas about Shrink !
            if(not_is_shrink_text_shown)
            {
                if(shrink_counter == 0 && textMeshProWin.text == "" && textMeshProDraw.text == "")
                {
                    textMeshProStart.text = "";
                    textMeshProShrink.text = "Shrink !";
                }
                shrink_counter += delta_time;
                if(shrink_counter >= shrink_wait)
                {
                    // stop display of shrink text
                    textMeshProShrink.text = "";
                    not_is_shrink_text_shown = false;
                }
            }
            // Platform shrinking logic.
            speed = max_speed;
            if (transform.localScale.x > min_scale && transform.localScale.z > min_scale)
            {
                new_x = transform.localScale.x - shrink_rate * delta_time;
                new_z = transform.localScale.z - shrink_rate * delta_time;
                transform.localScale = new Vector3(new_x, old_y, new_z);
            }
        }
        transform.Rotate(0, speed * delta_time, 0);
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
}