using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerPlayerController : MonoBehaviour
{
    public bool controlEnabled;
    private Vector3 spawnPosition;
    private FroggerUpdateVerticalLimits limitUpdater;

    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    public Sprite deadSprite;


    private void Awake()
    {
        spawnPosition = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Move Commands
        if (controlEnabled)
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
            {
                MovePlayer(Vector3.up);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
            {
                MovePlayer(Vector3.down);
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                MovePlayer(Vector3.right);
                transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                MovePlayer(Vector3.left);
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }

    }


    private void MovePlayer(Vector3 direction)
    {
        Vector3 toPosition = transform.position + direction;

        Collider2D gameLimit = Physics2D.OverlapBox(toPosition, Vector2.zero, 0f, LayerMask.GetMask("GameLimit"));
        Collider2D platform = Physics2D.OverlapBox(toPosition, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(toPosition, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        if (gameLimit != null)
        {
            return;
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obstacle != null && platform == null)
        {
            Die();
        }

        StartCoroutine(MoveAnimation(toPosition));
    }

    private IEnumerator MoveAnimation(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float duration = 0.2f;

        Vector3 maxScale = new Vector3(0.8f, 1.2f, 0.8f);
        Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);

        controlEnabled = false;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            if (t < duration / 2)
            {
                transform.localScale = Vector3.Lerp(minScale, maxScale, t/2);
            } else
            {
                transform.localScale = Vector3.Lerp(maxScale, minScale, t / 2);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = minScale;
        transform.position = endPos;
        controlEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EndGame>())
        {
            other.gameObject.GetComponent<EndGame>().FroggerArcadeCompleted();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            Die();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("CameraUpdater"))
        {
            limitUpdater = other.GetComponent<FroggerUpdateVerticalLimits>();
            limitUpdater.MoveCameraUp();
        }
        
    }


    private void Die()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        controlEnabled = false;
        spriteRenderer.sprite = deadSprite;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Invoke(nameof(Respawn), 1.5f);
    }

    private void Respawn()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        Debug.Log("Die");
        controlEnabled = true;
        spriteRenderer.sprite = defaultSprite;
        limitUpdater.ResetLimits();
    }
}
