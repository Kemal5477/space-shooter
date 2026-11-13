using UnityEngine;

public class BonusController : MonoBehaviour
{
   [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float bottomY = -5.5f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        HandleBottomBoundary();

        
    }
    void HandleBottomBoundary(){
        if(this.transform.position.y<bottomY){
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Player")){
            //triple shot etkinleştir
            PlayerController pc = other.transform.GetComponent<PlayerController>();
            if(pc!=null){
                pc.EnableTripleShot();
            }
            Destroy(this.gameObject);
        }
    }
}
