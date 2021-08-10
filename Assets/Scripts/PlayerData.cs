using UnityEngine;

[CreateAssetMenu(menuName = "Players/Standart Players", fileName = "New Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Transform _snakeTail;
    public Transform SnakeTail
    {
        get { return _snakeTail; }
        protected set { }
    }

    [SerializeField] private Transform _snakeHead;
    public Transform SnakeHead
    {
        get { return _snakeHead; }
        protected set { }
    }

    [SerializeField] private float _speedPlayer;
    public float SpeedPlayer
    {
        get { return _speedPlayer; }
        protected set { }
    }
}