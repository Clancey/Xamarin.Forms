using System;
using System.ComponentModel;
using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct Vector2
	{
		public Vector2(float x, float y)
			: this()
		{
			X = x;
			Y = y;
		}

		public Vector2(PointF p)
			: this()
		{
			X = p.X;
			Y = p.Y;
		}

		public Vector2(float angle)
			: this()
		{
			X = (float)Math.Cos(Math.PI * angle / 180);
			Y = (float)Math.Sin(Math.PI * angle / 180);
		}

		public float X { private set; get; }
		public float Y { private set; get; }

		public float LengthSquared
		{
			get { return X * X + Y * Y; }
		}

		public float Length
		{
			get { return (float)Math.Sqrt(LengthSquared); }
		}

		public Vector2 Normalized
		{
			get
			{
				float length = Length;

				if (length != 0)
				{
					return new Vector2(X / length, Y / length);
				}
				return new Vector2();
			}
		}

		public static float AngleBetween(Vector2 v1, Vector2 v2)
		{
			return 180 * (float)(Math.Atan2(v2.Y, v2.X) - Math.Atan2(v1.Y, v1.X)) / (float)Math.PI;
		}

		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static PointF operator +(Vector2 v, PointF p)
		{
			return new PointF(v.X + p.X, v.Y + p.Y);
		}

		public static PointF operator +(PointF p, Vector2 v)
		{
			return new PointF(v.X + p.X, v.Y + p.Y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static PointF operator -(PointF p, Vector2 v)
		{
			return new PointF(p.X - v.X, p.Y - v.Y);
		}

		public static Vector2 operator *(Vector2 v, float d)
		{
			return new Vector2(d * v.X, d * v.Y);
		}

		public static Vector2 operator *(float d, Vector2 v)
		{
			return new Vector2(d * v.X, d * v.Y);
		}

		public static Vector2 operator /(Vector2 v, float d)
		{
			return new Vector2(v.X / d, v.Y / d);
		}

		public static Vector2 operator -(Vector2 v)
		{
			return new Vector2(-v.X, -v.Y);
		}

		public static explicit operator PointF(Vector2 v)
		{
			return new PointF(v.X, v.Y);
		}

		public override string ToString()
		{
			return string.Format("({0} {1})", X, Y);
		}
	}
}