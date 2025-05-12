using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;

    private float shootTimer = 0f;
    private Transform playerTransform;

    protected override void Start()
    {
        base.Start();
        Hp = 2;
        Damage = 1;
        Speed = 2;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Move(Vector2 force)
    {
        rb.MovePosition(rb.position + force);

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    private void Shoot()
    {
        if (playerTransform == null || projectilePrefab == null || firePoint == null)
            return;

        Vector2 direction = playerTransform.position - firePoint.position;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(direction);
    }

    protected override void Attack(Player player)
    {
        player.TakeDamage(Damage, gameObject);
    }
}

