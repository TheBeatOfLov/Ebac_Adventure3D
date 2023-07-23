using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public KeyCode keyshoot = KeyCode.Z;
    public Transform positionToShoot;
    public float timeBetweenShots = .5f;

    private Coroutine _currentCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots); 
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }
}
