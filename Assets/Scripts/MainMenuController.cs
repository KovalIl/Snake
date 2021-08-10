using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _bestScoreText;

    void Start()
    {
        if (_playButton)
            _playButton.onClick.AddListener(PlayButton);
        if (_shopButton)
            _shopButton.onClick.AddListener(ShopButton);
        if (_settingsButton)
        {
            _settingsButton.onClick.AddListener(ToSettings);
        }
        if (_exitButton)
            _exitButton.onClick.AddListener(ExitButton);
        _bestScoreText.text = "Your best score: " + SaveLoadManager.Instance.CurrentSave.BestScore;
    }

    private void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ShopButton()
    {
        SceneManager.LoadScene("ShopMenu");
    }

    private void ToSettings()
    {

    }

    private void ExitButton()
    {
        Application.Quit();
    }
}
