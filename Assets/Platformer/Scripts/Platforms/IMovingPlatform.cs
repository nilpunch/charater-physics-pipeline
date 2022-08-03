using UnityEngine;

namespace Platformer
{
	public interface IMovingPlatform
	{
		public void ForwardVelocityTo(IPhysics physics);
	}
}