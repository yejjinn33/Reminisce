using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (!musicStart && myAudio != null)
        {
            if (collision.CompareTag("Note"))
            {
                Debug.Log("Music triggered!");
                myAudio.Play(); // À½¾Ç Àç»ý
                musicStart = true;
            }
        }
    }
}