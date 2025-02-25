using UnityEngine;
public class soundcript : MonoBehaviour
{   AudioSource mySource;
    void Start()
    {
     mySource = GetComponent<AudioSource>();
     mySource.Play();
    }
}
