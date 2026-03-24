using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    private Vector3 spawnPosition;

    private void Awake()
    {
        spawnPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Vector3.Distance(spawnPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            other.gameObject.GetComponent<SpaceInvadersEnemy>().wave.CheckEnemiesAlive();
        }
    }

}
