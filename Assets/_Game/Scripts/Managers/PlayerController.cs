using _Game.Scripts.Model;
using _Game.Scripts.Utilities.Extensions;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class PlayerController : MonoBehaviourSingletonPersistent<PlayerController>
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private Camera _mainCamera;
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
            _animator = gameObject.GetComponentInChildren<Animator>();

            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance == null || GameManager.Instance.GameOver)
            {
                return;
            }

            string anim;

            switch (_action)
            {
                case PlayerAction.Attack:
                    anim = "Attack";
                    break;

                case PlayerAction.Jump:
                case PlayerAction.Run:
                    anim = "Run";
                    break;
                default:
                    anim = "Idle";
                    break;
            }

            if (_animator is not null && !_animator.IsAnimationPlaying(anim))
            {
                _animator.Play(anim);
            }

            if (transform.position.x > 0 && _mainCamera.transform.position.x != transform.position.x)
            {
                _mainCamera.transform.position = new Vector2(transform.position.x, 0);
            }
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance == null  || GameManager.Instance.GameOver)
            {
                return;
            }

            var moveHorizontal = Input.GetAxisRaw("Horizontal");

            PlayerMove(moveHorizontal)
                .PlayerJump(moveHorizontal);
        }

        private PlayerController PlayerMove(float moveHorizontal)
        {
            if (moveHorizontal == 0)
            {
                _action = PlayerAction.Idle;
                return this;
            }

            if (_isJumping)
            {
                return this;
            }

            _action = PlayerAction.Run;

            transform.DOScaleX(moveHorizontal, 0);
            _rb.DOMoveX(_moveSpeed * Time.deltaTime * moveHorizontal + transform.position.x, Time.deltaTime);

            return this;
        }

        private PlayerController PlayerJump(float moveHorizontal)
        {
            if (!Input.GetKey(KeyCode.Space) || _isJumping)
            {
                return this;
            }

            Debug.Log("Jump");
            _isJumping = true;

            var target = transform.position + new Vector3(moveHorizontal, 0);

            _rb.DOJump(target, 0.2f, 1, 1)
                .WaitForCompletion();

            Debug.Log("Jump end");
            _isJumping = false;

            return this;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Deathzone"))
            {
                GameManager.Instance.GameOver = true;
            }
        }
    }
}