using UnityEngine;

public enum ProductCondition
{
    NotBought,
    Bought,
    Selected
}

public class ShopElement : MonoBehaviour
{
    public ProductCondition productCondition;
    public int Price;
    public string SnakeName;
    public GameObject SnakePrefab;
}
