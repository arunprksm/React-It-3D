using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    
    private void Start()
    {
        optionPanel.SetActive(false);
        AudioManager.Instance.PlayMusic(Sounds.Music, this.gameObject);
    }
    
    public void PlayButton()
    {
        AudioManager.Instance.PlaySFX(Sounds.ButtonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OptionButton()
    {
        if (optionPanel.activeSelf)
        {
            AudioManager.Instance.PlaySFX(Sounds.ButtonBack);
            optionPanel.SetActive(false);
            return;
        }
        AudioManager.Instance.PlaySFX(Sounds.ButtonClick);
        optionPanel.SetActive(true);
    }
}
