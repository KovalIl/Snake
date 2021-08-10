using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _toPrevSnake;
    [SerializeField] private Button _toNextSnake;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _cashText;
    [SerializeField] private Text _toBuyText;
    [SerializeField] private Text _snakeName;
    [SerializeField] private GameObject _selectObject;
    [SerializeField] private GameObject _buyObject;
    [SerializeField] private GameObject _toBuyObject;
    public ShopElement[] SnakeIndex = new ShopElement[3];
    public int ActiveSnakeIndex = 0;
    private int _money;
    private static int _currentSelectSnake;
    public int CurrentSelectSnake
    {
        get { return _currentSelectSnake; }
        private set { }
    }
    private SaveLoadManager saveLoadManager;

    void Start()
    {
        SnakeIndex[SaveLoadManager.Instance.CurrentSave.CurrentSnakeIndex].productCondition = ProductCondition.Selected;
        TheCoise(0);
        SnakeIndex[0].productCondition = ProductCondition.Bought;
        saveLoadManager = SaveLoadManager.Instance;
        _money = saveLoadManager.CurrentSave.Money;
        var boughtSnakes = saveLoadManager.CurrentSave.SnakeBought;
        for (int i = 0; i < boughtSnakes.Count; i++)
            SnakeIndex[boughtSnakes[i]].productCondition = ProductCondition.Bought;
        _priceText.text = "Price: " + SnakeIndex[ActiveSnakeIndex].Price + "$";
        _cashText.text = "Your cash " + _money + "$";
        _snakeName.text = "    " + SnakeIndex[ActiveSnakeIndex].SnakeName;
        if (_buyButton)
            _buyButton.onClick.AddListener(BuyButton);
        if (_selectButton)
            _selectButton.onClick.AddListener(SelectButton);
        if (_toPrevSnake)
            _toPrevSnake.onClick.AddListener(PrevButton);
        if (_toNextSnake)
            _toNextSnake.onClick.AddListener(NextButton);
        if (_closeButton)
            _closeButton.onClick.AddListener(OnCloseButton);
    }

    private void PrevButton()
    {
        if (ActiveSnakeIndex > 0 && ActiveSnakeIndex <= 2)
            TheCoise(-1);
    }

    private void NextButton()
    {
        if (ActiveSnakeIndex >= 0 && ActiveSnakeIndex < 2)
            TheCoise(1);
    }

    private void TheCoise(int index)
    {
        SnakeIndex[ActiveSnakeIndex].SnakePrefab.SetActive(false);
        ActiveSnakeIndex += index;
        SnakeIndex[ActiveSnakeIndex].SnakePrefab.SetActive(true);
        _priceText.text = "Price: " + SnakeIndex[ActiveSnakeIndex].Price + "$";
        _snakeName.text = "    " + SnakeIndex[ActiveSnakeIndex].SnakeName;
        if (SnakeIndex[ActiveSnakeIndex].productCondition == ProductCondition.Bought)
        {
            _buyObject.SetActive(false);
            _selectObject.SetActive(true);
            SnakeIndex[ActiveSnakeIndex].productCondition = ProductCondition.Bought;
        }
        else if (SnakeIndex[ActiveSnakeIndex].productCondition == ProductCondition.Selected)
        {
            SnakeIndex[ActiveSnakeIndex].productCondition = ProductCondition.Selected;
            _selectObject.SetActive(false);
            _buyObject.SetActive(false);
        }
        else
        {
            _selectObject.SetActive(false);
            _buyObject.SetActive(true);
        }
    }

    private void BuyButton()
    {
        if (SnakeIndex[ActiveSnakeIndex].Price <= _money && SnakeIndex[ActiveSnakeIndex].productCondition == ProductCondition.NotBought)
        {
            _toBuyText.text = "   Congratulations !";
            StartCoroutine(ToBuyText(1.3f));
            _snakeName.text = "    " + SnakeIndex[ActiveSnakeIndex].SnakeName;
            _money -= SnakeIndex[ActiveSnakeIndex].Price;
            _cashText.text = "Your cash " + _money + "$";
            SnakeIndex[ActiveSnakeIndex].productCondition = ProductCondition.Bought;
            TheCoise(0);
            saveLoadManager.SaveGame(ActiveSnakeIndex, SaveType.Buy);
            saveLoadManager.SaveGame(_money, SaveType.Money);
        }
        else
        {
            _toBuyText.text = "!Not enough money :(";
            TheCoise(0);
            StartCoroutine(ToBuyText(0.7f));
            _snakeName.text = "    " + SnakeIndex[ActiveSnakeIndex].SnakeName;
        }
    }

    private void SelectButton()
    {
        SnakeIndex[saveLoadManager.CurrentSave.CurrentSnakeIndex].productCondition = ProductCondition.Bought;
        SnakeIndex[ActiveSnakeIndex].productCondition = ProductCondition.Selected;
        TheCoise(0);
        saveLoadManager.SaveGame(ActiveSnakeIndex, SaveType.CurrentSnake);
    }

    private void OnCloseButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator ToBuyText(float time)
    {
        _toBuyObject.SetActive(true);
        yield return new WaitForSeconds(time);
        _toBuyObject.SetActive(false);
    }
}
