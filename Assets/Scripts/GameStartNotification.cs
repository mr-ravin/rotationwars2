using UnityEngine;
using System.Collections;
using TMPro; 
public class GameStartNotification : MonoBehaviour
{   private TextMeshProUGUI textMeshProStart;
    private Canvas canvas;
    float wait_time = 3f;
    void Start()
    {
        canvas = transform.Find("Canvas")?.GetComponent<Canvas>();
        textMeshProStart = canvas.transform.Find("TextGameStatusStart")?.GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowStartGameStatus());
    }
    private IEnumerator ShowStartGameStatus()
    {
        yield return new WaitForSeconds(wait_time);
        textMeshProStart.text = ""; 
    }
}