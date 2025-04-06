using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.5f; // Cooldown entre dashes
    public float sprintDuration = 3f;
    public float gravity = -9.81f;
    public float jumpForce = 5f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isDashing = false;
    private bool canSprint = true;
    private bool canJump = true;
    private float sprintTime;
    private Vector3 lastMoveDirection;

    private float lastDashTime = -9999f; // Variable para almacenar el tiempo del �ltimo dash

    void Start()
    {
        controller = GetComponent<CharacterController>();
        sprintTime = sprintDuration;
    }

    void Update()
    {
        Move();
        ApplyGravity();
        HandleDash();
    }

    void Move()
    {
        if (isDashing) return;

        // Obtener la entrada de movimiento (horizontal y vertical)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de direcci�n en funci�n de la entrada del jugador
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Obtener la rotaci�n de la c�mara en el eje Y (horizontal), para calcular el movimiento relativo a la c�mara
        direction = cameraTransform.TransformDirection(direction); // Movimiento relativo a la c�mara
        direction.y = 0f; // Ignorar cualquier componente de rotaci�n en el eje Y

        if (direction.magnitude > 0)
            lastMoveDirection = direction; // Guardar la �ltima direcci�n de movimiento

        float speed = walkSpeed;

        // Manejar sprint
        if (Input.GetKey(KeyCode.LeftShift) && canSprint && direction.magnitude > 0)
        {
            speed = sprintSpeed;
            sprintTime -= Time.deltaTime;
            if (sprintTime <= 0)
                canSprint = false;
        }
        else if (sprintTime < sprintDuration)
        {
            sprintTime += Time.deltaTime;
            if (sprintTime >= sprintDuration)
                canSprint = true;
        }

        // Aplicar movimiento al personaje
        controller.Move(direction * speed * Time.deltaTime);

        // Manejar salto
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            velocity.y = jumpForce;
            canJump = false;
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Evitar que el jugador se quede flotando
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleDash()
    {
        // Verificar si ha pasado el tiempo necesario para realizar el dash
        if (Input.GetMouseButtonDown(1) && Time.time - lastDashTime >= dashCooldown && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time; // Registrar el tiempo del �ltimo dash

        Vector3 dashDirection = lastMoveDirection;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

    // Funci�n para resetear el personaje despu�s de respawnear
    public void Respawn(Vector3 respawnPosition)
    {
        // Restaurar la posici�n del jugador al punto de respawn
        transform.position = respawnPosition;
        velocity = Vector3.zero;
        canJump = true;

        // Aseg�rate de que el movimiento siempre se recalcula correctamente en funci�n de la c�mara
        lastMoveDirection = Vector3.zero;
    }
}
