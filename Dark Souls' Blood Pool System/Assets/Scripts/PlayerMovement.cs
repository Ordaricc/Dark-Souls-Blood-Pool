using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        Movement();

        if (transform.position.y < -3)
        {
            Death();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 0.02f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * 0.02f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 0.02f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * 0.02f;
        }
    }

    private void Death()
    {
        transform.position = Vector3.up;
        GetComponent<PlayerSoulsSystem>().OnPlayerDeath();
    }
}