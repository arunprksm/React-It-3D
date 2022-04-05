using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    
    private void Start()
    {
        optionPanel.SetActive(false);
        AudioManager.Instance.PlayMusic(Sounds.Music);
    }
    
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PlaySFX(Sounds.ButtonClick);
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
