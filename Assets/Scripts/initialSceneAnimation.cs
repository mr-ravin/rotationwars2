using UnityEngine;

public class initialSceneAnimation : MonoBehaviour
{
    private float delta_time = 0f;
    private float camera_y = 11.4f;
    private float factor_y = 21f, rotation_speed = 50f;

    void Update()
    {   
        if(transform.position.y < camera_y)
        {
            delta_time = Time.deltaTime;
            transform.Translate(0, factor_y * delta_time, 0, Space.World);
            transform.Rotate(0, rotation_speed * delta_time, 0);
        }
    }
}
