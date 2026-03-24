using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersPlayerController : MonoBehaviour
{
    public bool controlEnabled;
    public float speed = 10f;
    public float horizontalInput = 0;
    public float bounds = 7;
    private Vector3 boundsL;
    private Vector3 boundsR;

    public GameObject projectilePrefab;

    private void Awake()
    {
        boundsL = new Vector3(transform.position.x - 7, transform.position.y, transform.position.z);
        boundsR = new Vector3(transform.position.x + 7, transform.position.y, transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)
        {
            if (transform.position.x < boundsL.x)
            {
                transform.position = boundsL;
            }
            if (transform.position.x > boundsR.x)
            {
                transform.position = boundsR;
            }

            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
    }

}
