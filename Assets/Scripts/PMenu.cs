using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _toMainMenuButton;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _pauseButtonObj;

    void Start()
    {
        if (_pauseButton)
            _pauseButton.onClick.AddListener(PauseButton);
        if (_playButton)
            _playButton.onClick.AddListener(PlayButton);
        if (_toMainMenuButton)
            _toMainMenuButton.onClick.AddListener(ToMainMenuButton);
    }

    private void PauseButton()
    {
        _pausePanel.SetActive(true);
        _pauseButtonObj.SetActive(false);
        Time.timeScale = 0;
    }

    private void PlayButton()
    {
        _pausePanel.SetActive(false);
        _pauseButtonObj.SetActive(true);
        Time.timeScale = 1;
    }

    private void ToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
