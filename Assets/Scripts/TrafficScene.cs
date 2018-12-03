using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrafficScene : MonoBehaviour {

	private List<GameObject> houses;

	// Use this for initialization
	void Start () {
		//var copy = Instantiate( car );
		//copy.transform.position = copy.transform.position + new Vector3( 1, 0, 0 );
		houses = GameObject.FindGameObjectsWithTag( "House" ).ToList();
		StartCoroutine( CreateMoreCars() );
	}
	
	// Update is called once per frame
	void Update () {
		CheckForRoadHover();
	}

	private IEnumerator CreateMoreCars() {

		var carPrefab = Resources.Load<GameObject>( "Car" );
		//var housePrefab = Resources.Load<GameObject>( "House" );

		while ( true ) {
			var car = Instantiate( carPrefab );
			var carController = car.GetComponent<AgentController>();
			var waypoints = houses.OrderBy( x => Guid.NewGuid() ).Take( 2 ).ToList();
			//Debug.Log( string.Join( ", ", waypoints.Select( w => w.name ) ) );
			//Debug.Log( $"??{waypoints.First().name} {waypoints.Last().name}" );
			carController.SetStartHouse( waypoints.First() );
			carController.SetWaypoint( waypoints.Last());
			yield return new WaitForSeconds(2 );
		}
	}

	private void CheckForRoadHover()
	{
		//Debug.Log( Input.mousePosition );
		var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		//Debug.Log( $"Ray: {ray.origin} {ray.direction}" );
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000, ~LayerMask.NameToLayer("Road"))) {
			var roadController = hit.collider.gameObject.GetComponent<RoadController>();
			roadController.Hover();
		}
	}
}
