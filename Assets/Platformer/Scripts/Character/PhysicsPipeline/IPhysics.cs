using UnityEngine;

namespace Platformer
{
	public static class PhysicsExtensions
	{
		public static float ContributingInDepenetration(this IPhysics physics, Vector3 force)
		{
			return Mathf.Sqrt(Mathf.Clamp(Vector3.Dot(force, physics.Depenetration) / 2f, 0f, float.MaxValue));
		}
	}
	
	public interface IPhysics
	{
		public Vector3 Position { get; }
		
		/// <summary>
		/// Velocity without constant force.
		/// </summary>
		public Vector3 Velocity { get; }
		
		/// <summary>
		/// Velocity with constant force.
		/// </summary>
		public Vector3 AbsoluteVelocity { get; }
		
		/// <summary>
		/// Depenetration from physics engine, applied to correct velocity.
		/// </summary>
		public Vector3 Depenetration { get; }
		
		/// <summary>
		/// Add velocity, that not affected by the acceleration.
		/// </summary>
		/// <param name="velocity">Force as velocity change.</param>
		public void AddConstantForce(Vector3 velocity);
		
		/// <summary>
		/// Add velocity that be affected by the acceleration. 
		/// </summary>
		/// <param name="velocity">Force as velocity change.</param>
		public void AddForce(Vector3 velocity);
	}
}