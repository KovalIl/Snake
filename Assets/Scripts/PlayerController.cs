using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float distanceBettwinTails;
    private Transform _snakeHead;
    private Transform _snakeTail;
    private List<Transform> _snakeTails = new List<Transform>();
    private List<Vector3> _positions = new List<Vector3>();
    [SerializeField] private AudioSource _fruitSound;
    [SerializeField] private AudioSource _gameOverSound;
    [SerializeField] private GameObject _scorePanel;
    public PlayerData[] playerData = new PlayerData[3];
    public Pooler Pooler;

    private void Awake()
    {
        _snakeTail = playerData[SaveLoadManager.Instance.CurrentSave.CurrentSnakeIndex].SnakeTail;
        _snakeHead = playerData[SaveLoadManager.Instance.CurrentSave.CurrentSnakeIndex].SnakeHead;
        _positions.Add(gameObject.transform.position);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = (gameObject.transform.position - _positions[0]).magnitude;
        if (distance > distanceBettwinTails)
        {
            Vector3 direction = (gameObject.transform.position - _positions[0]).normalized;
            _positions.Insert(0, _positions[0] + direction * distanceBettwinTails);
            _positions.RemoveAt(_positions.Count - 1);
            distance -= distanceBettwinTails;
        }
        gameObject.transform.Translate(Vector3.forward * 3f * Time.deltaTime);
        for (int i = 0; i < _snakeTails.Count; i++)
        {
            _snakeTails[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], distance / distanceBettwinTails);
            _snakeTails[i].rotation = gameObject.transform.rotation;
        }
    }

    private void Turn(float angle)
    {
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, angle, 0));
        gameObject.transform.rotation = quaternion;
    }

    public void RotateSnakeHead(SwipeDerection swipeDerection)
    {
        switch (swipeDerection)
        {
            case SwipeDerection.Down:
                Turn(-90f);
                break;
            case SwipeDerection.Up:
                Turn(90f);
                break;
            case SwipeDerection.Left:
                Turn(0);
                break;
            case SwipeDerection.Right:
                Turn(180f);
                break;
        }
    }

    public void AddTail()
    {
        Transform tail = Instantiate(_snakeTail, _positions[_positions.Count - 1], gameObject.transform.rotation, transform);
        _snakeTails.Add(tail);
        _positions.Add(tail.position);
        tail.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fuit"))
        {
            AddTail();
            _fruitSound.Play();
            GameManager.Instance.AddScore();
            ObjectType type = other.GetComponent<ObjectType>();
            Pooler.ReturnToPool(type ? type.Type : EnemyType.AppleFruit, other.gameObject);
            FruitSpawn.Instance.SpawnFruit();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _gameOverSound.Play();
            Destroy(gameObject);
            _scorePanel.SetActive(true);
            GameManager.Instance.ReturnMenu();
        }
    }
}