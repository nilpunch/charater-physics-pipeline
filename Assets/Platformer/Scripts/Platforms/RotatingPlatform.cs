using System;
using UnityEngine;

namespace Platformer
{
	public class RotatingPlatform : MonoBehaviour, IMovingPlatform
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Vector3 _rotationDirection = Vector3.up;
		[SerializeField] private float _rotationSpeed = 1f;

		private Vector3 _rotationAxis;
		
		private void Awake()
		{
			_rotationAxis = _rigidbody.rotation * _rotationDirection;
		}

		public void ManualUpdate()
		{
			float rotationSpeedDegrees = _rotationSpeed * Mathf.Rad2Deg;
			
			_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotationAxis * rotationSpeedDegrees * Time.deltaTime));
		}

		public void ForwardVelocityTo(IPhysics physics)
		{
			float angularVelocity = Vector3.Distance(Vector3.Scale(_rigidbody.position, Vector3.one - _rotationAxis),
				Vector3.Scale(physics.Position, Vector3.one - _rotationAxis)) * _rotationSpeed;
			Vector3 velocityTangent =
				Vector3.Cross(Vector3.Normalize(_rigidbody.position - physics.Position), _rotationAxis);
			physics.AddConstantForce(velocityTangent * angularVelocity);
		}
	}
}