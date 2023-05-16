using _Game.Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class Mob : MonoBehaviour
{
     private Rigidbody2D _rb;

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameOver)
        {
            return;
        }

        AttackToPlayer();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void AttackToPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        var rotate = player.transform.position.x > transform.position.x ? 1 : -1;

        transform.DOScaleX(rotate, 0);

        var offset = player.transform.position - transform.position;

        _rb.DOMove(transform.position + Time.deltaTime * offset, Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died");

            GameManager.Instance.GameOver = true;
        }
    }

    public Mob Spawn(Vector3 position)
    {
        return Instantiate(this, position, Quaternion.identity);
    }
}