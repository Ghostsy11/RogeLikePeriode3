using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "VFX", menuName = "Particle Effect", order = 1)]
public class VFX : ScriptableObject
{

    [Header("Player Or Enemy Hitting EachOther")]
    [SerializeField] ParticleSystem particle;

    [Header("Random VFX When Bullet hit each other")]
    [SerializeField] ParticleSystem[] particles;

    public void PlayVFX(Transform location)
    {
        PlayPartilceEffect(particle, location.gameObject.transform);
    }

    private void PlayPartilceEffect(ParticleSystem particle, Transform pos)
    {
        if (particle != null)
        {
            ParticleSystem VFX = Instantiate(particle, pos.gameObject.transform.position, Quaternion.identity);
            Destroy(VFX.gameObject, VFX.main.duration + VFX.main.startLifetime.constantMax);
        }
    }
    public void PlayARandomParticleEffect(Transform pos)
    {
        if (particles != null)
        {
            var randomParticle = Random.Range(0, particles.Length);
            var par = Instantiate(particles[randomParticle], pos.transform.position, Quaternion.identity);
            par.Play();
            Destroy(par, 5);

        }
    }

}
