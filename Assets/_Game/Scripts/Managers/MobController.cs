using Unity.VisualScripting;
using UnityEngine;

public class MobController : MonoBehaviourSingleton<MobController>
{
    private readonly int _maxMob = 3;

    [SerializeField]
    private Mob _mobPrefab;

    private bool _spawned;

    public void Spawn(Vector3 position)
    {
        if (_spawned)
        {
            return;
        }

        _spawned = true;

        for (int i = 1; i <= _maxMob; i++)
        {
            var mob = _mobPrefab.Spawn(position + new Vector3(Random.value + i, 0));

            var avatar = Resources.Load<Sprite>($"Sprites/Units/Mob/paimon");
            mob.AddComponent<SpriteRenderer>().sprite = avatar;            
        }
    }

    internal void Clear()
    {
        var mobs = FindObjectsOfType<Mob>();
        foreach (var item in mobs)
        {
            Destroy(item);
        }
    }
}