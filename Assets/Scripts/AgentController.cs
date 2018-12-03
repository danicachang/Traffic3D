using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour {
	
	private NavMeshAgent _agent;
	private GameObject _startHouse;

	// Use this for initialization
	void Start () {

		//StartCoroutine( ToggleWaypoints() );
	}

	/*private IEnumerator ToggleWaypoints()
	{
		var waypointIndex = 0;
		while ( true ) {

			_agent.SetDestination( Waypoints[waypointIndex].transform.position );
			waypointIndex++;

			if (waypointIndex == Waypoints.Length) {
				waypointIndex = 0;
			}
			yield return new WaitForSeconds( 5 );
		}
	}*/

	// Update is called once per frame
	void Update () {
		
	}

	public void SetStartHouse( GameObject startHouse ) {
		_agent = this.gameObject.GetComponent<NavMeshAgent>();
		_startHouse = startHouse;
		_agent.enabled = false;
		this.transform.position = _startHouse.transform.GetChild( 0 ).position;
	}

	public void SetWaypoint ( GameObject waypoint ) {
		_agent.enabled = true;
		_agent.SetDestination( waypoint.transform.position );
	}

	public void OnTriggerEnter( Collider collider ) {
		if ( collider.gameObject.CompareTag( "House" ) && collider.gameObject != _startHouse)
			Destroy( this.gameObject );
	}
}
