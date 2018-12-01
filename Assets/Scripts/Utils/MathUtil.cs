using UnityEngine;

public static class MathUtil {

	private static Vector3 vector = new Vector3();

	/// <summary>
	/// Gets the angle in degrees based on two positions.
	/// </summary>
	public static float GetAngleBasedOnPosition(Vector3 currentPosition, Vector3 targetPosition)
	{
		Vector2 direction = (targetPosition - currentPosition).normalized;
		return (Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
	}

	/// <summary>
	/// Gets the angle in degrees based on two positions.
	/// </summary>
	public static float GetAngleBasedOnPosition(Vector2 point)
	{
		Vector2 direction = (point - Vector2.zero).normalized;
		return (Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);
	}

	/// <summary>
	/// Gets the position x and y (from origin) based on an angle.
	/// </summary>
	public static Vector3 GetPositionBasedOnAngle(float angle, Vector3 origin)
	{
		vector.Set(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), vector.z);
		return vector + origin;
	}

	/// <summary>
	/// Gets the position x and y (from origin (0, 0)) based on an angle.
	/// </summary>
	public static Vector3 GetPositionBasedOnAngle(float angle)
	{
		vector.Set(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), vector.z);
		return vector + Vector3.zero;
	}

	/// <summary>
	/// Tells whether the two given vectors has approximate values on the x and y axis
	/// </summary>
	public static bool IsApproximate(Vector2 current, Vector2 target, float tolerance = .1f)
	{
		bool isWithinX = false;
		bool isWithinY = false;

		bool differentSignX = false;
		bool differentSignY = false;

		if(Mathf.Sign(target.x) != Mathf.Sign(current.x)) {
			isWithinX = (Mathf.Abs(target.x) + Mathf.Abs(current.x)) <= tolerance;
			differentSignX = true;
		}

		if(Mathf.Sign(target.y) != Mathf.Sign(current.y)) {
			isWithinY = (Mathf.Abs(target.y) + Mathf.Abs(current.y)) <= tolerance;
			differentSignY = true;
		}

		if(!differentSignX)
			isWithinX = Mathf.Abs(target.x - current.x) <= tolerance;

		if(!differentSignY)
			isWithinY = Mathf.Abs(target.y - current.y) <= tolerance;

		return isWithinX && isWithinY;
	}

	public static float GetDifference(float valueA, float valueB) {
		if(Mathf.Sign(valueA) != Mathf.Sign(valueB))
			return Mathf.Abs(valueA) + Mathf.Abs(valueB);

		return Mathf.Abs(valueA - valueB);
	}
}
