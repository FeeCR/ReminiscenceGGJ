using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceInvadersWaveMovement : MonoBehaviour
{
    public bool enableEnemyMovement;
    public float speed;
    private int direction;
    public GameObject[] enemies;
    private Vector3 defaultWavePosition;

    public UnityEvent onKillAll;

    private void Awake()
    {
        defaultWavePosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableEnemyMovement)
        {
            transform.Translate(Vector3.right * direction * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GameLimit"))
        {
            direction = direction * (-1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f , transform.position.z);
        }
    }

    public void ResetWave()
    {
        transform.position = defaultWavePosition;

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

    }

    public void CheckEnemiesAlive()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                return;
            }
        }
        enableEnemyMovement = false;
        onKillAll.Invoke();
    }

}
