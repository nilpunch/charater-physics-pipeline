using UnityEngine;

namespace Platformer
{
	public class CharacterFoot : MonoBehaviour
	{
		[SerializeField] private FootRaycast _footRaycast;
		[SerializeField, Range(0f, 90f)] private float _maxGroundAngle = 40f;
		[SerializeField, Min(0f)] private float _maxGroundDistance = 0.1f;
		[SerializeField] private float _cayoteTime = 0.1f;

		private float _elapsedAirTime;
		
		public bool IsOnGround { get; private set; }
		public bool IsInAir => !IsOnGround;

		public void UpdateGroundState()
		{
			bool onGround = false;

			if (_footRaycast.FootHit.HasHit)
			{
				float angleToGround = Vector3.Angle(Vector3.up, _footRaycast.FootHit.CalculateAverageNormal());
				
				// Ground must have less then specified angle to be threaten like a ground
				bool hitGroundAtExceptedAngle = angleToGround < _maxGroundAngle;
				bool hitGroundAtExceptedDistance = _footRaycast.FootHit.CalculateAverageDistance() < _maxGroundDistance;
				onGround = hitGroundAtExceptedAngle && hitGroundAtExceptedDistance;
			}
			
			if (onGround)
			{
				IsOnGround = true;
				_elapsedAirTime = 0f;
			}
			else if (_elapsedAirTime < _cayoteTime)
			{
				_elapsedAirTime += Time.deltaTime;
				IsOnGround = true;
			}
			else
			{
				IsOnGround = false;
			}
		}
	}
}