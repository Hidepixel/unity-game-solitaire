using UnityEngine;
using System.Collections;

public class ParticlesPlayer : MonoBehaviour 
{
	public ParticleSystem particles;

	public void ShowParticles(int count = 10)
	{
		particles.Emit(count);
	}

	public void StartContinuousEmission()
	{
		particles.Play();
	}

	public void StopEmission()
	{
		particles.Stop();
	}

    public void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
			if (!particles.isPlaying)
				particles.Play();
			else
				particles.Stop();
	}
}
