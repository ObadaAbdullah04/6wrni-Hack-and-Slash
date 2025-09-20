using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform Player;
    public int Score;
    public int LevelItems;
    public AudioClip[] levelSounds;
    public Transform[] Particles;
    public Transform MainCanvas;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip sound , Vector3 ownerPos)

    {
        GameObject obj = SoundPooler.current.GetPooledObject();
        AudioSource audio = obj.GetComponent<AudioSource>();

        obj.transform.position = ownerPos;
        obj.SetActive(true);
        audio.PlayOneShot(sound);
        StartCoroutine(DisableSound(audio));
    }
    IEnumerator DisableSound(AudioSource audio)
    { 
        while(audio.isPlaying)
        {
            yield return new WaitForSeconds(0.3f);
        }
        audio.gameObject.SetActive(false) ;
    }     
}
