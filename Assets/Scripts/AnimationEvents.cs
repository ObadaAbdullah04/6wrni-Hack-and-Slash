using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerControll ChrMove;
    public void PlayerAttack()
    {
        Debug.Log("Player Attacked !");
        ChrMove.DoAttack();
        LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[2], LevelManager.instance.Player.position);
    }
    public void PlayerDamage()
    {
        transform.GetComponentInParent<EnemyControll>().DamagePlayer();
    }
    public void MoveSound()
    {
        LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[0], LevelManager.instance.Player.position);
    }
}
