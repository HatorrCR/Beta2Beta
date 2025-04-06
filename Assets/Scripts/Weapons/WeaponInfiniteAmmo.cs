using UnityEngine;

public class WeaponInfiniteAmmo : MonoBehaviour
{
    public Transform firePoint; // Punto desde donde se dispara
    public GameObject bulletPrefab; // Prefab de la bala
    public float fireRate = 0.1f; // Tiempo entre disparos
    public float bulletSpeed = 20f; // Velocidad de la bala
    public float maxRange = 100f; // Distancia máxima del disparo
    public Camera playerCamera; // Cámara del jugador
    private float nextFireTime = 0f;
    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>(); // Obtener referencia al WeaponManager
    }

    void Update()
    {
        // Verificar que el arma infinita esté activa
        if (weaponManager != null && !weaponManager.IsInfiniteWeaponActive())
            return; // No disparar si el arma infinita no está activa

        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Verificar si las referencias necesarias están asignadas
        if (playerCamera == null || bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Falta asignar la cámara, el proyectil o el punto de disparo.");
            return;
        }

        // Obtener la dirección del disparo
        Vector3 shootDirection = GetCrosshairDirection();

        // Instanciar la bala en el firePoint
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Obtener el script Bullet de la bala instanciada
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = 1; // Aquí puedes ajustar el daño si es variable
        }

        // Configurar la velocidad de la bala
        Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootDirection * bulletSpeed;
        }


        // Establecer el tiempo del siguiente disparo
        nextFireTime = Time.time + fireRate;
    }

    Vector3 GetCrosshairDirection()
    {
        // Obtener el centro de la pantalla
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Lanzar un raycast desde la cámara hacia el centro de la pantalla
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRange))
        {
            // Si el raycast golpea algo, disparamos hacia ese punto
            return (hit.point - firePoint.position).normalized;
        }
        else
        {
            // Si no golpea nada, disparamos en la dirección de la cámara
            return ray.direction;
        }
    }
}
