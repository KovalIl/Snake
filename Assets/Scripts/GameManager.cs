using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _ReturnScoreText;
    [SerializeField] private Text _ReturnCashText;
    public int BestScore;
    private int _score;
    private int _cash;
    public PlayerData[] playerData = new PlayerData[3];
    [SerializeField] private Transform _player;

    private void Start()
    {
        _scoreText.text = "Score:  " + _score;
        Transform playerHead = playerData[SaveLoadManager.Instance.CurrentSave.CurrentSnakeIndex].SnakeHead;
        var child = Instantiate(playerHead, _player.transform.position, _player.rotation);
        child.SetParent(_player);
    }

    public void AddScore()
    {
        _score += 4;
        _scoreText.text = "Score: " + _score;
    }

    public void ReturnMenu()
    {
        _ReturnScoreText.text = "Your score: " + _score;
        _cash = _score / 2;
        _ReturnCashText.text = "Your cash: " + _cash + "$";
        if(SaveLoadManager.Instance.CurrentSave.BestScore < _score)
        {
            SaveLoadManager.Instance.SaveGame(_score, SaveType.Score);
        }
        SaveLoadManager.Instance.SaveGame(_cash + SaveLoadManager.Instance.CurrentSave.Money, SaveType.Money);
    }
}