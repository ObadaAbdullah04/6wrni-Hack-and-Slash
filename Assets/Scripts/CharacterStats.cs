using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 10;
    public float power = 1;
    int killScore = 20;
    public float currentHealth { get; private set; }
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth+value,0,maxHealth);
        Debug.Log("Current Health "+currentHealth+"/"+maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        if (transform.CompareTag("Enemy"))
        {
            transform.Find("Canvas").GetChild(1).GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        }
        else if (transform.CompareTag("Player"))
        { 
            LevelManager.instance.MainCanvas.Find("PanelStats").Find("ImageHealthBar").GetComponent<Image>().fillAmount = currentHealth / maxHealth;

        }
    }
    void Die()
    {
        if (transform.CompareTag("Player"))
        {
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[4], LevelManager.instance.Player.position);
        }
        else if (transform.CompareTag("Enemy"))
        {
            LevelManager.instance.Score += killScore;
            Instantiate(LevelManager.instance.Particles[2],transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
