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

    private void Update()
    {
        if (Input.GetKeyDown(keyshoot))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(keyshoot))
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots); 
        }
    }

    private void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }
}
