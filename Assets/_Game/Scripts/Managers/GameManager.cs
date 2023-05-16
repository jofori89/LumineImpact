using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Managers
{
    public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
    {
        [field: SerializeField] public bool GameOver { get; set; }

        [SerializeField] private Button _btnStart;

        public override void Awake()
        {
            base.Awake();
            GameOver = true;
        }

        private void Start()
        {
            _btnStart.onClick.AddListener(StartGame);
            _btnStart.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (GameOver && !_btnStart.gameObject.activeSelf)
            {
                _btnStart.gameObject.SetActive(true);
            }
        }

        private void StartGame()
        {
            _btnStart.gameObject.SetActive(false);
            GameOver = false;

            PlayerController.Instance.Spawn();
            MobController.Instance.Clear();
        }
    }
}