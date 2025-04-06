using UnityEngine;

public class MissileSimpleAlign : MonoBehaviour
{
    public Transform missileMesh; // El mesh visual del misil

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 moveDirection = transform.position - lastPosition;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            missileMesh.rotation = Quaternion.LookRotation(moveDirection.normalized);
        }

        lastPosition = transform.position;
    }
}
