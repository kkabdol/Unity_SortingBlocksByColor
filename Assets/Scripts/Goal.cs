using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
	public AudioClip audioGood;
	public AudioClip audioBad;

	private BlockController blockController;

	void Awake ()
	{
		blockController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<BlockController> ();
	}

	public void SetColor (Color color)
	{
		renderer.material.color = color;
	}

	public Color GetColor ()
	{
		return renderer.material.color;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag.Equals (Tags.block)) {

			Block block = other.GetComponent<Block> ();
			if (block.GetColor () == GetColor ()) {
				audio.PlayOneShot (audioGood);
				blockController.IncreaseScore ();
				Destroy (other.gameObject, 2f / other.rigidbody.velocity.magnitude);
			} else {
				audio.PlayOneShot (audioBad);
				blockController.ResetSpeed ();
				Destroy (other.gameObject);
			}
		}
	}
}
