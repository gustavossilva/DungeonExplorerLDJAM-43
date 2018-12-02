using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[SerializeField] private Transform _playerPosition;
	[SerializeField] private float cameraSpeed = 2f;
	public Vector3 cameraOffset;

	private Vector3 _positionToGo;
	private Vector3 lerpPosition;
	// Use this for initialization
	void Start () {
	}
	 
	// Update is called once per frame
	void LateUpdate () {
		_positionToGo = _playerPosition.position + cameraOffset;
		lerpPosition = Vector3.Lerp(this.transform.position,_positionToGo,cameraSpeed * Time.deltaTime);
		this.transform.position = lerpPosition;
	}
}
