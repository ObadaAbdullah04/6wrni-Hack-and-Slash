using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyControll : MonoBehaviour
{
    CharacterStats stats;
    //public Transform Player;
    NavMeshAgent agent;
    Animator animator;
    public float AttackRaduis = 5f;
    bool canAttack = true;
    float AttackCooldown =3f;
    //
    int counter = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator= GetComponentInChildren<Animator>();
        stats = GetComponentInChildren<CharacterStats>();
    }


    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude);
        float Distance = Vector3.Distance(transform.position,LevelManager.instance.Player.position);
        if(Distance<AttackRaduis)
        { 
            agent.SetDestination(LevelManager.instance.Player.position);
            if (Distance <= agent.stoppingDistance)
            {
                if (canAttack)
                {
                   StartCoroutine(cooldown());
                    //Play Attack Animation
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
    IEnumerator cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Contacted !! ");
            stats.ChangeHealth(-other.GetComponentInParent<CharacterStats>().power);
            //Destroy(gameObject);
        }
    }
    public void DamagePlayer()
    {
        LevelManager.instance.Player.GetComponent<CharacterStats>().ChangeHealth(-stats.power);
        if (counter < 4)
        {
            Onlytimes();
            counter++;
        }
    }
    void Onlytimes()
    {
         LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[3], LevelManager.instance.Player.position);
    }
}