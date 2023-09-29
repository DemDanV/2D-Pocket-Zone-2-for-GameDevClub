using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = InputManager.Controls.Player.Move.ReadValue<Vector2>();
        Vector3 moveValue = input;
        moveValue = moveValue * moveSpeed * Time.deltaTime;
        transform.position += moveValue;
    }
}
