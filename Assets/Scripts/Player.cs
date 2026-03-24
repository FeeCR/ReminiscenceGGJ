using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Transform orientation;

    [SerializeField]
    GameController gameController;

    private float horizontalInput, verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;

    bool canInteract;
    float rangeToInteract = 5;

    bool canControlPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControlPlayer) return;

        HandleInput();

        ClampSpeed();

    }

    private void FixedUpdate()
    {
        if (!canControlPlayer) return;

        MovePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if(moveDirection != Vector3.zero)
        {
            AudioController.instance.TriggerPlayerAS(true);
        }
        else
        {
            AudioController.instance.TriggerPlayerAS(false);
        }
    }

    private void ClampSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void DelegatePlayerControl(bool shouldGiveControlToPlayer)
    {
        canControlPlayer = shouldGiveControlToPlayer;
    }
}
