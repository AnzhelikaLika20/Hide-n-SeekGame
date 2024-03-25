using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        //transform.position += Time.deltaTime * moveSpeed * moveDirection;
        //fieldOfView.SetAimDirection(transform.forward);
        //fieldOfView.StartingAngle = transform.rotation.eulerAngles.y;
        //fieldOfView.Origin = transform.position;
    }

    private void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        characterController.Move(moveSpeed * Time.deltaTime * moveDirection);
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * moveSpeed);
    }
}