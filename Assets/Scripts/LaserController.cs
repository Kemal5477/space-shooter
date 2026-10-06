using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f; // 0.5f lazer için genelde yavaş kalabilir, hızı artırabilirsiniz
    [SerializeField] float topBound = 7.3f; // Ekranın üst sınır değeri

    void Update()
    {
        this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Lazer ekranın üst sınırını geçtiğinde sahneden silinir
        if (transform.position.y > topBound)
        {
            Destroy(gameObject);
        }
    }
}