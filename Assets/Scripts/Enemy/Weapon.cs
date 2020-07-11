using Lean.Pool;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] private Transform shootPoint;
    
    
    [Tooltip("скорость полета пули")] public float buletSpeed;
    [Tooltip("дамаг дял пули")] public float bulletDamage;
    [Tooltip("тип пули")] public BulletType bulletType;

    [Tooltip("размера магазиня для пуль")] public int magazineSize;
    [Tooltip("время перезорядки оружия")] public float reloadTime;
    [Tooltip("время между выстрелами")] public float fireRate;

    int magazineBulletLeft;

    private void Start()
    {
        magazineBulletLeft = magazineSize;
    }

    public void Fire()
    {
        
        magazineBulletLeft--;
        
        Bullet bullet = LeanPool.Spawn(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.FireBullet(bulletDamage, buletSpeed);
    }
    
    public void Reload()
    {
        magazineBulletLeft = magazineSize;
    }

    public bool NeedsReload()
    {
        return magazineBulletLeft <= 0;
    }
}
