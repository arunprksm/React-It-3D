using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(Sounds.Scene01, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
