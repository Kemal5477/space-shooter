using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float spawnY = 8.0f;       // Ekranın üst dış noktası
    [SerializeField] float bottomBound = -7.0f; // Ekranın alt çıkış noktası
    float spawnX;

    void Start()
    {
        Respawn();
    }

    void Update()
    {
        // Dümdüz aşağı iniş
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);

        // Ekranın altına ulaştığında tekrar tepeye ışınlanır
        if (transform.position.y < bottomBound)
        {
            Respawn();
        }
    }

    // Hem ilk başta hem de aşağı düştükçe çağrılan yeniden doğma metodu
    void Respawn()
    {
        spawnX = Random.Range(-9.54f, 9.54f);
        transform.position = new Vector3(spawnX, spawnY,this.transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            //Damage the palyer
            PlayerController pController=other.transform.GetComponent<PlayerController>();
            pController.TakeDamage();
            //Destroy yourself
            Destroy(this.gameObject);
        
        }
        else if(other.CompareTag("Laser")){
        //Add points 
        Destroy(other.gameObject);
        //Destroy yourself
       Destroy(this.gameObject);
        }
    }
}