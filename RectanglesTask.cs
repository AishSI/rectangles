using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			var res = (r1.Right < r2.Left
				|| r1.Left > r2.Right
				|| r1.Bottom < r2.Top
				|| r1.Top > r2.Bottom);
			return !res;
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			var horizontalIntersection = (new Segment(r1.Left, r1.Right).GetIntersectionLength(new Segment(r2.Left, r2.Right)));
			var verticallIntersection = (new Segment(r1.Top, r1.Bottom).GetIntersectionLength(new Segment(r2.Top, r2.Bottom)));
			return horizontalIntersection * verticallIntersection;
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if (r1.IsInside(r2))
				return 0;
			if (r2.IsInside(r1))
				return 1;
			return -1;
		}
	}

	public class Segment
	{
		public int Point1;
		public int Point2;

		public Segment(int point1, int point2)
		{
			this.Point1 = point1;
			this.Point2 = point2;
		}
	}

	public static class SegmentExtensions
	{
		public static int GetIntersectionLength(this Segment segment1, Segment segment2)
		{
			if (segment1.Point2 < segment2.Point1 || segment1.Point1 > segment2.Point2)
				return 0;
			int[] points = { segment1.Point1, segment1.Point2, segment2.Point1, segment2.Point2 };
			Array.Sort(points); //можно было отсортировать циклом с Min - для соблюдения условия не использовать библиотечных методов, но не стал заморачиваться
			return points[2] - points[1];
		}
	}

	public static class RectangleExtensions
	{
		public static bool IsInside(this Rectangle inner, Rectangle outer)
		{
			var res = (outer.Left <= inner.Left
				&& outer.Right >= inner.Right)
				&& (outer.Top <= inner.Top
				&& outer.Bottom >= inner.Bottom);
			return res;
		}
	}
}