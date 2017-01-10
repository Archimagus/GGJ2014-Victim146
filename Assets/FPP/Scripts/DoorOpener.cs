using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour
{
	public int RequiredKey=-1;
	public float StayOpenTime = 2.0f;
	public Collider DoorCollider;
	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;
	float closeTime;
	Animation doorAnim;
	void Start()
	{
		if (animation != null)
			doorAnim = animation;
		else
			doorAnim = GetComponentInChildren<Animation>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && (RequiredKey == -1 || other.GetComponent<Player>().HasKey(RequiredKey)))
		{
			OpenDoor();
		}
	}
	void OpenDoor()
	{
		if (Time.time > closeTime)
		{
			foreach (AnimationState state in doorAnim)
			{
				state.speed = 1;
			}
			DoorCollider.enabled = false;
			doorAnim.Play();
			this.PlaySoundEffect(doorOpenSound, SoundType.SoundEffect);
			closeTime = Time.time + StayOpenTime;
			Invoke("CloseDoor", StayOpenTime);
		}
	}

	void CloseDoor()
	{
		foreach (AnimationState state in doorAnim)
		{
			state.speed = -1;
		}
		this.PlaySoundEffect(doorCloseSound, SoundType.SoundEffect);
		doorAnim.Play();
		DoorCollider.enabled = true;
	}

}
