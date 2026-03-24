using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersEnemy : MonoBehaviour
{
    public SpaceInvadersWaveMovement wave;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteIndex = 0;
        StartCoroutine(ChangeSprite());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GameOverLimit"))
        {
            wave.ResetWave();
        }
    }

    private IEnumerator ChangeSprite()
    {
        while(gameObject.activeSelf)
        {
            var toSprite = spriteIndex == 0 ? 1 : 0;
            spriteRenderer.sprite = sprites[toSprite];
            spriteIndex = toSprite;
            yield return new WaitForSeconds(1f);
        }
        yield return null;

    }

}
