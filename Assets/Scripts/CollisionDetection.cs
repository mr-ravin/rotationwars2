using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro; 
public class CollisionDetection : MonoBehaviour
{   
    private Canvas canvas;
    private TextMeshProUGUI textMeshProWin;
    private TextMeshProUGUI textMeshProDraw;
    private TextMeshProUGUI textMeshProShrink;
    private string status_text = "";
    private int player_collided = 0;
    private float status_wait_time = 2.5f, scene_load_wait_time = 5.0f;
    private string sceneNameToLoad = "FirstScene";
    void Start()
    {
        canvas = GameObject.Find("GameTriggers")?.transform.Find("Canvas")?.GetComponent<Canvas>();
        textMeshProWin = canvas.transform.Find("TextGameStatusWin")?.GetComponent<TextMeshProUGUI>();
        textMeshProDraw = canvas.transform.Find("TextGameStatusDraw")?.GetComponent<TextMeshProUGUI>();
        textMeshProShrink = canvas.transform.Find("TextGameStatusShrink")?.GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter(Collider other_collision)
    {   
        if(other_collision.gameObject.tag == "player1")
        {
                player_collided += 1;
        }
        if(other_collision.gameObject.tag == "player2")
        {
                player_collided += 2;
        } 
        StartCoroutine(SetStatusText());
        StartCoroutine(WaitBeforeRestart());
    }
    private IEnumerator SetStatusText()
    {   textMeshProShrink.text = "";
        if(status_wait_time!=0f) // when first collision is made, wait some time to show the status text.
        {   
            yield return new WaitForSeconds(status_wait_time);
            status_wait_time = 0f; // incase another player also hits the ground, status should be shown before new scene-load.
        }
        switch(player_collided)
        {
            case 1: 
                    status_text = "Player 2 Wins!";
                    textMeshProWin.SetText(status_text);
                    break;
            case 2:
                    status_text = "Player 1 Wins!";
                    textMeshProWin.SetText(status_text);
                    break;
            case 3:
                    status_text = "It's a Draw!";
                    textMeshProWin.SetText("");
                    textMeshProDraw.SetText(status_text);
                    break;
            default: 
                    status_text = "";
                    textMeshProWin.SetText(status_text);
                    textMeshProDraw.SetText(status_text);
                    break;
        }
    }
    private IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSeconds(scene_load_wait_time);
        SceneManager.LoadScene(sceneNameToLoad); // Restart Game
    }
}