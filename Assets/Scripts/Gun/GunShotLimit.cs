using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotLimit : GunBase
{
    public float maxShots = 5;
    public float timeToRecharge = 1f;

    private float _currentShot;
    private bool _recharging = false;

    protected override IEnumerator ShootCoroutine()
    {
        /* while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots); 
        }*/
        if (_recharging) yield break;
        while (true)
        {
            if(_currentShot < maxShots)
            {
                Shoot();
                _currentShot++;
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShots);
            }

        }
    }

    private void CheckRecharge()
    {
        if(_currentShot >= maxShots)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _currentShot = 0;
        _recharging = false;
    }
}
