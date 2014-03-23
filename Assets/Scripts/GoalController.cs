using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour
{
	public Goal[] goals;

	public void Reset ()
	{
		int count = goals.Length;
		if (count != Block.colors.Length) {
			Debug.Log ("There is something wrong with count of goals and block colors");
			return;
		}
		
		List<Color> clist = new List<Color> (count);
		for (int i=0; i<count; ++i) {
			clist.Add (Block.colors [i]);
		}
		
		for (int i=0; i<count; ++i) {
			int ci = Random.Range (0, clist.Count);
			goals [i].SetColor (clist [ci]);
			clist.RemoveAt (ci);
		}
	}
}
