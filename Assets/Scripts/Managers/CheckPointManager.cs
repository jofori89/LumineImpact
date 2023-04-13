using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MobController.Instance.Spawn(gameObject.transform.position + new Vector3(1, 0, 0));
        }
    }
}
