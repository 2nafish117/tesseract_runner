using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventToolKit : MonoBehaviour
{

    //FindObjectOfType<EventToolKit>().ShowParticleEffectOn("Hit",transform,2);

    [Header("Particle Effects")]
    public GameObject[] allPartileEffects;
    public string[] allParticleEffectsTag;
    List<GameObject> currentParticleEffect=new List<GameObject>();

    [Header("Sound Effects")]
    public AudioSource mainAudioSource;
    private AudioSource tempAudioSource;
    public AudioClip[] allAudioClips;
    public string[] allAudioClipName;
    List<AudioClip> currentaudioClip = new List<AudioClip>();



    public static EventToolKit instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void SetDefaultAudioSourceIfNotExist()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>())
            mainAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    #region Particle Effects

    int GetparticleEffects(string tagname)
    {
        currentParticleEffect.Clear();
        int d = 0;
        foreach (GameObject g in allPartileEffects)
        {
            if (allParticleEffectsTag[d] == tagname)
            {
                currentParticleEffect.Add(g);
            }
            d++;
        }
        int randomNu = Random.Range(0, currentParticleEffect.Count);
        return randomNu;
    }


    public void ShowParticleEffectWithSoundEffect(string tagname,Vector3 position,Transform objectrotation,float destroyIn,string soundname)
    {
        int randomNu = GetparticleEffects(tagname);
        GameObject go = Instantiate(currentParticleEffect[randomNu], position, objectrotation.rotation);
        PlayAnySoundWithTag(soundname);
        Destroy(go, destroyIn);
    }

    public void ShowParticleEffectWithOutSoundEffect(string tagname, Vector3 position, Transform objectrotation, float destroyIn)
    {
        int randomNu = GetparticleEffects(tagname);
        GameObject go = Instantiate(currentParticleEffect[randomNu], position, objectrotation.rotation);
        Destroy(go, destroyIn);
    }

    #endregion

    #region Audio Effects

    int GetAudioEffects(string tagname)
    {
        currentaudioClip.Clear();
        int d = 0;
        foreach (AudioClip g in allAudioClips)
        {
            if (allAudioClipName[d] == tagname)
            {
                currentaudioClip.Add(g);
            }
            d++;
        }
        int randomNu = Random.Range(0, currentaudioClip.Count);
        return randomNu;
    }

    public void DoorSoundEffect()//make Door tags for audio
    {
        int randomNu = GetAudioEffects("Door");
        if (mainAudioSource == null)
            SetDefaultAudioSourceIfNotExist();
        tempAudioSource = mainAudioSource;
        tempAudioSource.PlayOneShot(currentaudioClip[randomNu], 0.7F);
    }

    public void WaterSoundEffect(GameObject go)//make Water tags for audio
    {
        tempAudioSource=go.AddComponent<AudioSource>();
        int randomNu = GetAudioEffects("Water");
        tempAudioSource.spatialBlend = 1f;
        tempAudioSource.maxDistance = 10;
        tempAudioSource.dopplerLevel = 0.5f;
        tempAudioSource.PlayOneShot(currentaudioClip[randomNu], 1F);
    }

    public void PlayAnySoundWithTag(string tagname,AudioSource tempSource = null)
    {
        int randomNu = GetAudioEffects(tagname);
        if (tempSource != null)
            tempAudioSource = tempSource;
        else
        {
            if (mainAudioSource == null)
                SetDefaultAudioSourceIfNotExist();
            tempAudioSource = mainAudioSource;
        }
            
        tempAudioSource.PlayOneShot(currentaudioClip[randomNu], 0.7F);
    }


    public void MakeAudioSource(GameObject go)
    {
        if (!go.GetComponent<AudioSource>())
            go.AddComponent<AudioSource>();
    }

    public void MakePlayAndDestroyAudioSource(GameObject go,string tagname,float timetodestroy)
    {
        if(!go.GetComponent<AudioSource>())
            go.AddComponent<AudioSource>();
        PlayAnySoundWithTag(tagname, go.GetComponent<AudioSource>());
        Destroy(go.GetComponent<AudioSource>(), timetodestroy);
    }

    #endregion

    #region Destruction Effects
                                                                                                                                                   //if none then no particle effect                 if none then no sound effect
    public void OnDestructionEffect(GameObject orignalObject,GameObject DestructableObject,float explosionForce,float radius,float duration,string particleEffectName ,float particleEffectDuration,string soundEffectName,float soundEffectDuration)
    {
        orignalObject.SetActive(false);
        GameObject newObject = Instantiate(DestructableObject, orignalObject.transform.position, orignalObject.transform.rotation);
        foreach(Transform t in newObject.transform)
        {
            t.gameObject.AddComponent<Rigidbody>();
            t.gameObject.AddComponent<BoxCollider>();
            Destroy(t, duration + 1);
        }
        newObject.AddComponent<Rigidbody>();
        newObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, newObject.transform.position, radius);
       


        if(particleEffectName!="none")//do the particle effect
        {
            ShowParticleEffectWithOutSoundEffect(particleEffectName, newObject.transform.position, newObject.transform, particleEffectDuration);
        }

        if (soundEffectName != "none")//do the particle effect
        {
            MakePlayAndDestroyAudioSource(newObject, soundEffectName, soundEffectDuration);
        }

        Destroy(orignalObject, duration);
        Destroy(newObject, duration + 2);
    }

    #endregion

    #region Animation Effect

    public void AnimationEffectWithOutSoundEffect(Animator anim,string clipName,float animation_speed=1)
    {
        anim.Play(clipName);
        anim.speed = animation_speed;
    }

    public void AnimationEffectWithSoundEffect(Animator anim, string clipName,string soundname,float soundDuration,float animation_speed = 1)
    {
        anim.Play(clipName);
        anim.speed = animation_speed;
        MakePlayAndDestroyAudioSource(anim.gameObject, soundname, soundDuration);
    }

    #endregion


}
