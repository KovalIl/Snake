using UnityEngine;

public class ObjectType : MonoBehaviour
{
    [SerializeField] private EnemyType type;
    public EnemyType Type
    { get
        {
            return type;
        }
        set { type = value; }
    }
}
