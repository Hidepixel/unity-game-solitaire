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
}
