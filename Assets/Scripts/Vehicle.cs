using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

	public Vector3 position;
	public Vector3 direction;
	public Vector3 velocity;
	public Vector3 acceleration;

	public float mass;
	public float maxSpeed;
	public float maxForce;
	public float radius;

	public float maxAvoidDistance;

	public Material fVectorColor;
	public Material rVectorColor;
	public Material futurePosVectorColor;

	public bool debugEnabled = false;

	public GameObject terrain;

	private float maxHeight = 0f;
	private bool CanCheck = true;
	private bool OnGround = false;

	// Use this for initialization
	protected virtual void Start () {
		//float height = terrain.GetComponent<Terrain> ().SampleHeight (transform.position);
		//transform.position = new Vector3 (transform.position.x, -.09f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		UpdatePosition ();
		SetTrasform ();
		/*if (CanCheck) {
			CheckBelow ();
		}*/

		float terrainheight;
	}

	protected Vector3 Seek (Vector3 targetPos) {
		Vector3 desiredVelocity = targetPos - position;
		desiredVelocity.Normalize ();
		desiredVelocity = desiredVelocity * maxSpeed;

		return (desiredVelocity - velocity);
	}

	protected Vector3 Flee (Vector3 targetPos) {
		Vector3 desiredVelocity = position - targetPos;
		desiredVelocity.Normalize ();
		desiredVelocity = desiredVelocity * maxSpeed;

		return (desiredVelocity - velocity);
	}

	protected Vector3 Pursue (Vector3 targetPos){
		return Seek (targetPos * 2f);
	}

	protected Vector3 Evade (Vector3 targetPos){
		return Flee (targetPos * 2f);
	}

	abstract protected void CalcSteeringForces();

	void UpdatePosition () {
		position = gameObject.transform.position;
		velocity += acceleration;
		/*if (OnGround) {
			velocity.y = 0;
		}*/
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);
		position += velocity;
		direction = velocity.normalized;
		acceleration = Vector3.zero;
	}

	void SetTrasform () {
		gameObject.transform.right = -direction;
		//position = new Vector3 (position.x, 0.1f, position.z);
		transform.position = position;
	}

	void ApplyForce (Vector3 force) {}

	protected Vector3 JAvoid(){
		GameObject[] gArr = GameObject.FindGameObjectsWithTag ("avoid");
		List<GameObject> currentObstacles = new List<GameObject>();

		for (int i = 0; i < gArr.Length; i++)
		{
			if(LocationCheck(gArr[i].transform.position, gArr[i].transform.forward.sqrMagnitude))
			{
				currentObstacles.Add(gArr[i]);
			}
		}

		foreach(GameObject obj in currentObstacles)
		{
			Vector3 distToObstacle = obj.transform.position - transform.position;
			float dot = Vector3.Dot(transform.right, distToObstacle);

			if (dot >= 0)
			{
				return (-1 * transform.right) * maxSpeed;
			}
			else if(dot < 0)
			{
				return transform.right * maxSpeed;
			}
		}

		return Vector3.zero;
	}

	private bool LocationCheck(Vector3 obsPosition, float obsRadius)
	{
		Vector3 futureLocation = transform.forward * maxAvoidDistance;

		Vector3 distToObstacle = obsPosition - transform.position;

		if(distToObstacle.sqrMagnitude < futureLocation.sqrMagnitude)
		{
			if(Vector3.Dot(distToObstacle, transform.right) < obsRadius + radius)
			{
				if(Vector3.Dot(distToObstacle, transform.forward) > 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	//

    public Vector3 Wander ()
    {
		Vector3 cCenter = (transform.position + transform.forward.normalized);

		float rAng = Random.Range (0f, 360f);
		//rAng = Mathf.PerlinNoise (cCenter.x, Time.deltaTime);

		Vector3 target = new Vector3 (0, 0, 1f);
		target = Quaternion.AngleAxis (rAng, Vector3.up) * target;
		Debug.Log (target);
		target.Normalize ();
		Vector3 ctarget = cCenter + target;
		ctarget.y = 0;
		//Debug.Log (ctarget);

		return Seek(ctarget);
    }

	public Vector3 NoOOB (){
		if (transform.position.z >= 5f || transform.position.z <= -5f || transform.position.x >= 5f || transform.position.x <= -5f){
			return Seek (Vector3.zero);
		}
		return Vector3.zero;
	}

	public Vector3 Separation (GameObject obj){
		return Flee (obj.transform.position);
	}

	/*void CheckBelow(){
		RaycastHit2D rch = Physics2D.Raycast (transform.position, Vector2.down);
		maxHeight = rch.point.y;
	}

	void OnCollisionEnter2D(Collision2D col){
		CanCheck = false;
		velocity.y = 0;
		OnGround = true;
	}

	void OnCollisionStay2D(Collision2D col){
		Debug.Log ("collision detected");
		velocity.y = 0;
		//acceleration.y = 0;
		//CanGravity = false;
		transform.position = new Vector3(transform.position.x, maxHeight);
		Debug.Log ("maxheight: " + maxHeight);
		OnGround = true;
	}

	void OnCollisionExit2D(Collision2D col){
		//CanGravity = true;
		CanCheck = true;
		OnGround = false;
	}*/

}
