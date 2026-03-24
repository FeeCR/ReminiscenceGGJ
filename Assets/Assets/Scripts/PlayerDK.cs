using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDK : MonoBehaviour
{
    [SerializeReference] private LayerMask PlatformsLayerMask;
    //private Player_Base playerBase;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;
    public bool controlenabled;
    private int level;
    private int lives;
    private int score;
    public GameObject Spawner;

    [SerializeField]
    GameController controller;

    [SerializeField] Transform spawnPoint;
    private void Awake() {

       // playerBase = gameObject.GetComponent<Player_Base>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxcollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (controlenabled){
   
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float jumpvelocity = 9f;
            rigidbody2d.velocity = Vector2.up * jumpvelocity;
        }

        // transform.Translate(
        // Input.GetAxis("Horizontal") * 2f * Time.deltaTime,
        //Input.GetAxis("Vertical") * 0f * Time.deltaTime, 0f);
        HandleMovement();
    }
         }
    private bool IsGrounded() {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, .1f, PlatformsLayerMask);
        Debug.Log(raycasthit2d.collider);   
        return raycasthit2d.collider != null ;
        }


    private void HandleMovement()
    {
        if (controlenabled == true) { 
            float movespeed = 2f;
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2d.velocity = new Vector2(-movespeed, rigidbody2d.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.D)){
                rigidbody2d.velocity = new Vector2(+movespeed, rigidbody2d.velocity.y);
            } 
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objective"))
        {
            Destroy(Spawner);
            enabled = false;
            FindObjectOfType<PlayerDK>().LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Obstacle")) 
        {
            //enabled = false;
            //SceneManager.LoadScene("Level1");
            FindObjectOfType<PlayerDK>().LevelFailed();
        }
    }
    private void NewGame()
    {
        lives = 10;
        score = 0;

        LoadLevel(1);
    }

    public void LoadLevel(int index)
    {
        level = index;
        SceneManager.LoadScene(level);

        Camera camera = Camera.main;

        if (camera != null)
        {
            camera.cullingMask = 0;
        }
        Invoke(nameof(LoadScene), 0f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void LevelComplete()
    {
        controller.CompletedArcade(2);

        controlenabled = false;
        enabled = false;
    }
    public void LevelFailed()
    {

        transform.position = spawnPoint.position;

        return;

        lives--;

        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
            LoadLevel(level);
        }
    }

    public void SetControlEnabled(bool shouldControl)
    {
        Spawner.SetActive(true);
        controlenabled = shouldControl;
    }

}

