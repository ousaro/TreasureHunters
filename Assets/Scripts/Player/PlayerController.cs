using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float hSpeed = 5f;
    [SerializeField] private AnimationController playerAnimationController;


    private float _direction = 1;
    private Rigidbody2D _playerRigidBody;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0) _direction = Mathf.Sign(horizontalInput);

        _playerRigidBody.velocity = new Vector2(horizontalInput * hSpeed, _playerRigidBody.velocity.y);

        if(horizontalInput == 0)
        {
            playerAnimationController.ChangeCurrentAnimation("Idle");
        }
        else
        {
            playerAnimationController.ChangeCurrentAnimation("Run");
        }

        transform.localScale = new Vector3(_direction, 1, 1);
    }
}
