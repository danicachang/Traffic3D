using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadController : MonoBehaviour {

	public bool disabled = true;
	public Material disabledMaterial;
	public Material hoverDisabledMaterial;
	public Material hoverMaterial;
	public Material enabledMaterial;

	private float lastHoverTime;
	private bool isHover;

	private MeshRenderer currentRenderer;

	// Use this for initialization
	void Start () {
		disabled = true;
		lastHoverTime = -1000;
		currentRenderer = this.GetComponent<MeshRenderer>();
		updateMaterial( false );
	}
	
	// Update is called once per frame
	void Update () {
		var currentTime = Time.timeSinceLevelLoad;
		var isStillHover = currentTime - lastHoverTime < .1;

		if (isHover && !isStillHover ) {
			isHover = false;
			updateMaterial( false );
		}

	}

	void updateMaterial(bool hovering) {
		if ( hovering )
			currentRenderer.material = disabled ? hoverDisabledMaterial : hoverMaterial;
		else
			currentRenderer.material = disabled ? disabledMaterial : enabledMaterial;
	}

	void updateDisabled( bool newDisable ) {
		if ( disabled == newDisable ) return;

		disabled = newDisable;
		foreach (var obstacle in this.GetComponentsInChildren<NavMeshObstacle>() ) {
			obstacle.enabled = disabled;
		}

	}

	public void Hover() {
		if ( !isHover )
			updateMaterial( true );
		isHover = true;
		lastHoverTime = Time.timeSinceLevelLoad;

		if (Input.GetButtonDown("Mouse0")) {
			updateDisabled( !disabled );
			updateMaterial( true );
		}
	}
}
