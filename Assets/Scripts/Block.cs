using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
	public static Color[] colors = {
		Color.black,
		Color.blue,
		Color.green,
		Color.red,
		Color.white,
		Color.yellow,
		Color.magenta,
		Color.cyan
	};
	
	void Awake ()
	{
		renderer.material.color = colors [Random.Range (0, colors.Length)];
	}

	public Color GetColor ()
	{
		return renderer.material.color;
	}
}
