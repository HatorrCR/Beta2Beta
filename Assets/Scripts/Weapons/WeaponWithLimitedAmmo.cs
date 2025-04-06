using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponWithLimitedAmmo : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletSpeed = 20f;
    public float maxRange = 100f;
    public Camera playerCamera;

    [Header("Munición")]
    public int maxAmmo = 5;
    private int currentAmmo = 0;

    [Header("UI")]
    public RawImage specialAmmoIcon;
    public TMP_Text ammoText;

    private float nextFireTime = 0f;
    private WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        UpdateAmmoUI();
    }

    void Update()
    {
        if (weaponManager != null && weaponManager.IsInfiniteWeaponActive())
            return;

        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                UpdateAmmoUI();
            }
            else
            {
                Debug.Log("Sin munición en el arma especial.");
                UpdateAmmoUI();
            }
        }
    }

    void Shoot()
    {
        if (playerCamera == null || bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Falta asignar la cámara, el proyectil o el punto de disparo.");
            return;
        }

        // Obtener la dirección del disparo
        Vector3 shootDirection = GetCrosshairDirection();

        // Instanciar la bala en el firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Marcar la bala como especial
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.isSpecialBullet = true;
        }

        // Configurar la velocidad de la bala
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootDirection * bulletSpeed;
        }

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

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        Debug.Log($"Munición recogida: {amount}. Munición actual: {currentAmmo}");
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (specialAmmoIcon != null)
        {
            specialAmmoIcon.gameObject.SetActive(currentAmmo > 0);
        }

        if (ammoText != null)
        {
            if (currentAmmo > 1)
            {
                ammoText.gameObject.SetActive(true);
                ammoText.text = $" x{currentAmmo}";
            }
            else
            {
                ammoText.gameObject.SetActive(false);
            }
        }
    }
}
