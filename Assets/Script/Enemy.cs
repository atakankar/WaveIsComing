using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //düşmanın hızı
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    //düşmanın canı
    public float startHealth = 100;
    private float health;
    //düşmanı öldürme altını
    public int worth = 50;
    //düşmanın ölme efektti
    public GameObject enemyDeathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }   

    //Düşmanın Hasar alması 
    public void TakeDamage(float amont)
    {        
        health -= amont;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    //Düşmanın yavaşlatlması
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    
    void Die()
    {
        isDead = true;
        //Düşmanın yok edilmesi
        Destroy(gameObject);
        //yok edilem düşmanın ölüm efektti
        GameObject effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        //deneme
        WaveSpawner.EnemiesAlive--;
        //yok edilen düşmandan para kazanılması
        PlayerStats.Money += worth;        
    }

    
}
