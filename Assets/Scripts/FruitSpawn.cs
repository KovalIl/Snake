using UnityEngine;

public class FruitSpawn : Singleton<FruitSpawn>
{
    [SerializeField] private EnemyType[] _nextFruit;
    [SerializeField] private Pooler _pooler;
    private int fruitCount = -1;

    private void Start()
    {
        SpawnFruit();
    }

    public void SpawnFruit()
    {
        GameObject fruit = _pooler.GetGameObjectFromPool(ChooseNextFruit());
        if (fruit)
        {
            fruit.transform.position = new Vector3(Random.Range(-9.3f, 8.84f), -86.611f, Random.Range(9.27f, 18.3f));
            fruit.transform.rotation = Quaternion.identity;
            fruit.SetActive(true);
        }
    }

    private EnemyType ChooseNextFruit()
    {
        fruitCount += 1;
        if (fruitCount >= _nextFruit.Length)
        {
            fruitCount = 0;
        }
        if (_nextFruit.Length != 0 && fruitCount < _nextFruit.Length)
        {
            return _nextFruit[fruitCount];
        }
        return EnemyType.AppleFruit;
    }
}