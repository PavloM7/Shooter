using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    //[SerializeField] private UnityEvent IsShooting;

    private EnemyDeath target;

    [SerializeField] private Text ammoInMagazineText;

    [HideInInspector] public void Shoot(int _damage)
    {
        if (CheckShootingToEnemy())
        {
            target = Raycasting.hit.collider.gameObject.GetComponent<EnemyDeath>();
            target.CheckShootAndDamage(_damage);
        }
    }

    private bool CheckShootingToEnemy()
    {
        if (Physics.Raycast(Raycasting.ray) && Raycasting.hit.collider.gameObject.CompareTag("Enemy"))
        {
            return true;
        }

        return false;
    }

    [HideInInspector] public void DrawAmmo(int ammoInMagazine)
    {
        ammoInMagazineText.text = ammoInMagazine.ToString();
    }
}