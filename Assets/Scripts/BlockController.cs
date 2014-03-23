using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
	public Block block;
	public Transform spawnPos;
	public float defaultSpeed = 5f;
	public float maxSpeed = 10f;
	public float defaultSpawnDelay = 0.5f;
	public float minSpawnDelay = 0.1f;
	public float maxSpeedLevel = 10f;
	public float timeLimit = 60f;

	private Block centerBlock;
	private float curSpeed;
	private float curSpawnDelay;
	private float curSpeedLevel;
	private TextMesh scoreLabel;
	private int score;
	private TextMesh timeLabel;
	private float timeLeft;
	private GoalController goalController;
	
	void Awake ()
	{
		centerBlock = null;
		scoreLabel = GameObject.FindGameObjectWithTag (Tags.score).GetComponent<TextMesh> ();
		timeLabel = GameObject.FindGameObjectWithTag (Tags.time).GetComponent<TextMesh> ();
		goalController = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<GoalController> ();
	}

	void Start ()
	{
		SpawnBlock ();
		Reset ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			RollBlock (Vector3.left);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			RollBlock (Vector3.right);
		} else if (Input.GetKeyDown (KeyCode.W)) {
			RollBlock (Vector3.forward);
		} else if (Input.GetKeyDown (KeyCode.X)) {
			RollBlock (Vector3.back);
		} else if (Input.GetKeyDown (KeyCode.Q)) {	// upper left
			RollBlock (new Vector3 (-1f, 0f, 1f));
		} else if (Input.GetKeyDown (KeyCode.E)) {	// upper right
			RollBlock (new Vector3 (1f, 0f, 1f));
		} else if (Input.GetKeyDown (KeyCode.C)) {	// bottom right
			RollBlock (new Vector3 (1f, 0f, -1f));
		} else if (Input.GetKeyDown (KeyCode.Z)) {  // bottom left
			RollBlock (new Vector3 (-1f, 0f, -1f));
		}

		// time limit
		if (timeLeft > 0f) {
			float newTime = Mathf.Max (timeLeft - Time.deltaTime, 0f);
			SetTime (newTime);

			if (newTime <= 0f) {
				Reset ();
			}
		}
	}

	public void Reset ()
	{
		SetTime (timeLimit);
		SetScore (0);
		goalController.Reset ();

		ResetSpeed ();
	}
	public void IncreaseScore ()
	{
		SetScore (score + 1);
		IncreaseSpeed ();
	}

	private void SetScore (int newScore)
	{
		score = newScore;
		scoreLabel.text = newScore.ToString ();
	}

	private void SetTime (float newTime)
	{
		timeLeft = newTime;
		System.Text.StringBuilder timeString = new System.Text.StringBuilder ();

		timeLabel.text = Mathf.Floor (newTime).ToString ();
	}

	public void ResetSpeed ()
	{
		curSpeedLevel = 0;
		curSpeed = defaultSpeed;
		curSpawnDelay = defaultSpawnDelay;
	}

	private void IncreaseSpeed ()
	{
		float old = curSpeedLevel;
		curSpeedLevel = Mathf.Min (curSpeedLevel + 1f, maxSpeedLevel);

		if (old != curSpeedLevel) {
			float val = curSpeedLevel / maxSpeedLevel;
			curSpeed = Mathf.Lerp (defaultSpeed, maxSpeed, val);
			curSpawnDelay = Mathf.Lerp (defaultSpawnDelay, minSpawnDelay, val);
		}
	}
	
	void SpawnBlock ()
	{
		if (centerBlock == null) {
			centerBlock = Instantiate (block, spawnPos.position, spawnPos.rotation) as Block;
		}
	}

	void RollBlock (Vector3 direction)
	{
		if (centerBlock == null) {
			return;
		}

		direction = Vector3.Normalize (direction);
		centerBlock.rigidbody.velocity = direction * curSpeed;
		centerBlock.rigidbody.rotation = Quaternion.Euler (direction * curSpeed);
		centerBlock = null;

		Invoke ("SpawnBlock", curSpawnDelay);
	}
}
