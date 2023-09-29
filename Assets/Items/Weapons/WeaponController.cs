using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Magazine magazine;
    [SerializeField] Transform shootOutRef;
    [SerializeField] float cooldownTimer = 0.5f;
    [SerializeField] Transform leftHandPlacementRef;
    [SerializeField] float reloadTime = 3;


    //[SerializeField] Transform leftHandLimbRef;

    public Magazine Magazine => magazine;


    EnemyLocator enemyLocator;
    Animator ownerAnimator;

    float lstShootTime;
    bool canShoot = true;//True only for DEBUG purposes

    private void Equip()
    {
        ownerAnimator.SetTrigger("Equip");
    }

    private void Unequip()
    {
        ownerAnimator.SetTrigger("Unequip");
        this.enabled = false;
    }


    private void Update()
    {
        if (enemyLocator.Target == null)
            return;

        // Получите позиции объекта и цели в мировых координатах
        Vector3 objectPosition = transform.position;
        Vector3 targetPosition = enemyLocator.Target.position;


        // Установите Z-координату объекта и цели на одну и ту же плоскость
        objectPosition.z = 0f;
        targetPosition.z = 0f;

        // Найдите направление на цель
        Vector3 lookDirection = targetPosition - objectPosition;
        Debug.Log(lookDirection + " : " + objectPosition);



        // Используйте Mathf.Atan2 для вычисления угла между направлением и осью X
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Поворот объекта в направлении цели
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //transform.LookAt(enemyLocator.Target);
    }

    public bool Shoot()
    {
        Debug.Log("Shoot WC1");

        if (canShoot == false) return false;

        if(enemyLocator.Target == null) return false; //???

        if (Time.time - lstShootTime < cooldownTimer) return false;

        lstShootTime = Time.time;

        bool haveAmmo = magazine.GetAmmo();

        if (haveAmmo)
        {
            Debug.Log("Shoot WC2");

            GameObject go = Instantiate(
                magazine.Ammo.Prefab,
                shootOutRef.position,
                shootOutRef.rotation);

            BulletController bullet = go.GetComponent<BulletController>();
            bullet.Initialize(enemyLocator.TargetEntity);
        }
        else
            Reload();

        return haveAmmo;
    }

    public bool Reload()
    {
        string message = magazine.CanReload;
        bool canReload = (message == "");
        if (canReload)
        {
            StartCoroutine(Reloading());
            //ownerAnimator.Play(R)
        }
        else
        {
            PlayerNotificationsManager.singleton.Notify(message);
        }
        return canReload;
    }

    IEnumerator Reloading()
    {
        canShoot = false;
        PlayerNotificationsManager.singleton.SetState("Reloading...", reloadTime);
        yield return new WaitForSeconds(reloadTime);
        EndReload();
    }

    public void EndReload()
    {
        canShoot = true;
        magazine.Reload();
    }

    internal void SetOwner(Entity owner)
    {
        Inventory ownerInventory = owner.GetComponent<Inventory>();
        ownerAnimator = owner.GetComponent<Animator>();
        enemyLocator = owner.EnemyLocator;

        UseInventory(ownerInventory);
    }

    internal void UseInventory(Inventory inventory)
    {
        magazine.UseInventory(inventory);
    }

    public void LoadFromSerializable(SerializableWeaponController from)
    {
        magazine.LoadFromSerializable(from.magazine);
    }
}

[Serializable]
public class SerializableWeaponController
{
    public SerializableMagazine magazine;

    public SerializableWeaponController(WeaponController controller)
    {
        magazine = new SerializableMagazine(controller.Magazine);
    }
}
