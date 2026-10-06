using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed=5.0f;
    Vector2 moveInput ;
    Vector2 xBounds=new(-9.23f,9.23f);
    Vector2 yBounds=new(-3.8f,-2.0f);

    [SerializeField] GameObject laserPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] float fireRate = 0.5f; // İki ateş arasındaki bekleme süresi (saniye)
    float nextFireTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(Vector3.right*moveSpeed*Time.deltaTime);
        Vector3 direction=new Vector3(moveInput.x,moveInput.y,0f);
        this.transform.Translate(direction * moveSpeed * Time.deltaTime,Space.World);
        
        ClampPosition();
    }

   public void OnMove(InputValue value){
    moveInput=value.Get<Vector2>();
    Debug.Log(moveInput);
   }

   void  ClampPosition(){
    Vector3 p=this.transform.position;
    p.x=Mathf.Clamp(p.x, xBounds.x,xBounds.y);
    p.y=Mathf.Clamp(p.y, yBounds.x,yBounds.y);
    this.transform.position=p;
   }

   public void OnFire()
{if (Time.time >= nextFireTime)
        {
            Debug.Log("Fire!");
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            // Bir sonraki atış hakkını şimdiki zaman + cooldown süresi olarak güncelliyoruz
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            // İsterseniz bekleme süresindeyken konsola bilgi basabilirsiniz (isteğe bağlı)
            // Debug.Log("Lazer henüz hazır değil!");
        }
}
}