using UnityEngine;

public class BurgerGun : MonoBehaviour
{
    [Header("Burger Settings")]
    [SerializeField] private GameObject burgerPrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float shootForce = 20f; 
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBurger();
        }
    }

    void ShootBurger()
    {
        if (burgerPrefab == null || firePoint == null) return;

        GameObject burger = Instantiate(burgerPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = burger.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }

        Destroy(burger, 5f);
    }
}
