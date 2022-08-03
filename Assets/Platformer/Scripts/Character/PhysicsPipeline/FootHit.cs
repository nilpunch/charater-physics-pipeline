using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Platformer
{
	public readonly struct FootHit
	{
		public readonly IReadOnlyList<RaycastHit> Hits;
		public readonly float RaysIndent;

		public FootHit(IReadOnlyList<RaycastHit> readOnlyList, float raysIndent)
		{
			Hits = readOnlyList;
			RaysIndent = raysIndent;
		}

		public bool HasHit => Hits.Any(raycastHit => raycastHit.collider is not null);
		
		public IEnumerable<Collider> Colliders => Hits
			.Where(hit => hit.collider is not null)
			.Select(hit => hit.collider)
			.Distinct();

		public Vector3 CalculateAverageNormal()
		{
			Vector3 normalSum = Vector3.zero;
			float distanceSum = 0f;
			int hits = 0;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				hits += 1;
				distanceSum += raycastHit.distance;
				normalSum += raycastHit.normal;
			}
			
			if (hits == 0)
				throw new Exception("There no ground hit.");

			return normalSum / hits;
		}
		
		public float CalculateAverageDistance()
		{
			Vector3 normalSum = Vector3.zero;
			float distanceSum = 0f;
			int hits = 0;
			
			foreach (var raycastHit in Hits.Where(raycastHit => raycastHit.collider is not null))
			{
				hits += 1;
				distanceSum += raycastHit.distance;
				normalSum += raycastHit.normal;
			}
			
			if (hits == 0)
				throw new Exception("There no ground hit.");

			return distanceSum / hits - RaysIndent;
		}
	}
}