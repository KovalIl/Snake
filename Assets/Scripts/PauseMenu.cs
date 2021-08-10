using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _returnButton;
    [SerializeField] private Button _toMenuButton;

    void Start()
    {
        if (_returnButton)
            _returnButton.onClick.AddListener(ToReturnButton);
        if (_toMenuButton)
            _toMenuButton.onClick.AddListener(ToMainMenuButton);
        GameManager.Instance.ReturnMenu();
    }

    private void ToReturnButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
