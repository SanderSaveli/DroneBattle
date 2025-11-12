using UnityEngine;

namespace Sander.DroneBattle
{
    public static class ColliderHelper
    {
        public static bool HasCollision(Vector2 vector2, Collider2D collider2D)
        {
            if (collider2D is BoxCollider2D box)
            {
                return Physics2D.OverlapBox(vector2 + box.offset, box.size, box.transform.eulerAngles.z);
            }
            else if (collider2D is CircleCollider2D circle)
            {
                return Physics2D.OverlapCircle(vector2 + circle.offset, circle.radius);
            }
            else if (collider2D is CapsuleCollider2D capsule)
            {
                return Physics2D.OverlapCapsule(vector2 + capsule.offset, capsule.size, capsule.direction, capsule.transform.eulerAngles.z);
            }
            else if (collider2D is PolygonCollider2D poly)
            {
                Vector2[] points = poly.points;
                for (int i = 0; i < points.Length; i++)
                    points[i] = vector2 + (Vector2)poly.transform.TransformVector(points[i] + poly.offset);

                return Physics2D.OverlapPoint(points[0]);
            }

            throw new System.ArgumentException("Can't check collider");
        }
    }
}
