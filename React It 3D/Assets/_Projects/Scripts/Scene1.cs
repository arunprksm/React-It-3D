using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(Sounds.Scene01, this.gameObject);
    }
    public void MainMenuScene()
    {
        AudioManager.Instance.PlaySFX(Sounds.ButtonBack);
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
