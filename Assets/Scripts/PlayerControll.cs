using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControll : MonoBehaviour
{
    CharacterStats stats;
    CharacterController Controller;
    public float Speed = 5f;
    Transform Cam;
    float Gravity = 10;
    float VertivalVelocity = 10;
    public float JumpValue = 10;
    Animator animator;
    int pinum = 0;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        stats= GetComponentInChildren<CharacterStats>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool IsSprint = Input.GetKey(KeyCode.LeftShift);
        float Sprint = IsSprint ? 1.85f : 1;
        Vector3 Movedirection = new Vector3(horizontal, 0, vertical);
        animator.SetFloat("Speed", Mathf.Clamp(Movedirection.magnitude, 0, 0.5f) + (IsSprint ? 0.5f : 0));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        if (Controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                VertivalVelocity = JumpValue;
            }
        }
        else
            VertivalVelocity -= Gravity * Time.deltaTime;

        // this IF statement is used to let the player direction where you leave it /NOT FOLLOWING THE CAM 
        if (Movedirection.magnitude > 0.1)
        {
            //high level code that controlls the camera angle :)
            float angle = Mathf.Atan2(Movedirection.x, Movedirection.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        Movedirection = Cam.TransformDirection(Movedirection);
        Movedirection = new Vector3(Movedirection.x * Speed * Sprint
            , VertivalVelocity, Movedirection.z * Speed * Sprint);

        Controller.Move(Movedirection * Time.deltaTime);
    }
    public void DoAttack()
    {
        //Activated the collider for ! seconds of time 
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            GetComponent<CharacterStats>().ChangeHealth(1);
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[1], LevelManager.instance.Player.position);
            Instantiate(LevelManager.instance.Particles[1], other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item"))
        {
            LevelManager.instance.LevelItems++;
            Debug.Log("Items : " + LevelManager.instance.LevelItems);
            LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[1], LevelManager.instance.Player.position);
            Instantiate(LevelManager.instance.Particles[0], other.transform.position, other.transform.rotation);
            pinum +=1;
            LevelManager.instance.MainCanvas.Find("PanelStats").Find("ammount").GetComponent<TextMeshProUGUI>().text = pinum.ToString();

            Destroy(other.gameObject);
        }
    }

}
