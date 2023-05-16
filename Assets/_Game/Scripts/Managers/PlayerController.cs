using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviourSingletonPersistent<PlayerController>
{
    private Rigidbody2D _rb;
    private Camera _mainCamera;

    private float _moveHorizontal;

    private PlayerAction _action;

    private int _moveSpeed = 2;

    private bool _isJumping;

    public void Spawn()
    {
        gameObject.transform.position = new Vector2(0, 0);
    }

    public void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.GameOver)
        {
            return;
        }

        _moveHorizontal = Input.GetAxisRaw("Horizontal");

        string path = "";

        switch (_action)
        {
            case PlayerAction.Attack:
                path = $"Sprites/Units/Player/Lumine_attack";
                break;

            case PlayerAction.Jump:
            case PlayerAction.Run:
                path = $"Sprites/Units/Player/Lumine_run";
                break;

            default:
                path = $"Sprites/Units/Player/Lumine_attack";
                break;
        }

        var avatar = Resources.Load<Sprite>(path);

        gameObject.GetComponent<SpriteRenderer>().sprite = avatar;

        if (transform.position.x > 0 && _mainCamera.transform.position.x != transform.position.x)
        {
            _mainCamera.transform.position = new Vector2(transform.position.x, 0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameOver)
        {
            return;
        }

        PlayerMove();
        PlayerJump();
    }

    private void PlayerMove()
    {
        if (_moveHorizontal == 0)
        {
            _action = PlayerAction.Attack;
            return;
        }

        if (_moveHorizontal != 0 && !_isJumping)
        {
            _action = PlayerAction.Run;

            transform.DOScaleX(_moveHorizontal, 0);
            _rb.DOMoveX(_moveSpeed * Time.deltaTime * _moveHorizontal + transform.position.x, Time.deltaTime);
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && !_isJumping)
        {
            Debug.Log("Jump");
            _isJumping = true;

            var target = transform.position + new Vector3(_moveHorizontal, 0);

            _rb.DOJump(target, 0.2f, 1, 1)
                .WaitForCompletion();

            Debug.Log("Jump end");
            _isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deathzone"))
        {
            GameManager.Instance.GameOver = true;
        }
    }
}

public enum PlayerAction
{
    Attack,
    Run,
    Jump
}