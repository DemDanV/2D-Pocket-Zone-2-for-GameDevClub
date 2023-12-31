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

    public Magazine Magazine => magazine;

    EnemyLocator enemyLocator;
    Animator ownerAnimator;

    float lstShootTime;
    bool reloading;

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

        // �������� ������� ������� � ���� � ������� �����������
        Vector3 objectPosition = transform.position;
        Vector3 targetPosition = enemyLocator.Target.position;


        // ���������� Z-���������� ������� � ���� �� ���� � �� �� ���������
        objectPosition.z = 0f;
        targetPosition.z = 0f;

        // ������� ����������� �� ����
        Vector3 lookDirection = targetPosition - objectPosition;


        // ����������� Mathf.Atan2 ��� ���������� ���� ����� ������������ � ���� X
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // ������� ������� � ����������� ����
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public bool Shoot()
    {
        if (reloading) return false;

        if (Time.time - lstShootTime < cooldownTimer) return false;

        if (magazine.NeedReload)
        {
            Reload();
            return false;
        }

        if (enemyLocator.Target == null) return false; //???

        bool tookAmmo = magazine.GetAmmo();

        if (tookAmmo)
        {
            lstShootTime = Time.time;

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

        return tookAmmo;
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
        reloading = true;
        PlayerNotificationsManager.singleton.SetState("Reloading...", reloadTime);
        yield return new WaitForSeconds(reloadTime);
        EndReload();
    }

    public void EndReload()
    {
        reloading = false;
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
