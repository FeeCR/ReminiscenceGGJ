using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerMoveObject : MonoBehaviour
{

    public Vector2 direction = Vector2.right;
    public float speed = 1f;
    public float size;

    public GameObject gameLimitL;
    public GameObject gameLimitR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        CheckOutOfBounds();
    }

    private void CheckOutOfBounds()
    {
        GameObject gameLimitEnd = direction.x < 0 ? gameLimitL : gameLimitR;
        GameObject gameLimitStart = direction.x > 0 ? gameLimitL : gameLimitR;

        float headOffset = (size / 2) * direction.x;
        float tailOffset = headOffset * (-1);

        if (direction.x < 0 && transform.position.x + tailOffset < gameLimitEnd.transform.position.x)
        {
            Vector3 respawnPos = new Vector3(gameLimitStart.transform.position.x - headOffset, transform.position.y, transform.position.z);
            Respawn(respawnPos);
        } else if (direction.x > 0 && transform.position.x + tailOffset > gameLimitEnd.transform.position.x)
        {
            Vector3 respawnPos = new Vector3(gameLimitStart.transform.position.x - headOffset, transform.position.y, transform.position.z);
            Respawn(respawnPos);
        }


    }

    private void Respawn(Vector3 startPos)
    {
        transform.position = startPos;
    }

}
