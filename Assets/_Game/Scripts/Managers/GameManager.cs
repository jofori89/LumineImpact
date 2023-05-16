using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    [field: SerializeField]
    public bool GameOver { get; set; } = true;

    [SerializeField]
    private Button _btnStart;

    private void Start()
    {
        _btnStart.onClick.AddListener(() => StartGame());
        _btnStart.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameOver)
        {
            _btnStart.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        _btnStart.gameObject.SetActive(false);
        GameOver = false;

        PlayerController.Instance.Spawn();
        MobController.Instance.Clear();
    }
}