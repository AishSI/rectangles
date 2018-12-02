using System;

namespace Rectangles
{

	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			// так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
			var result = (r1.Right < r2.Left
				|| r1.Left > r2.Right
				|| r1.Bottom < r2.Top
				|| r1.Top > r2.Bottom);
			return !result;
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			var horizontalIntersection = GetIntersectionLength(new Segment(r1.Left, r1.Right), new Segment(r2.Left, r2.Right));
			var verticallIntersection = GetIntersectionLength(new Segment(r1.Top, r1.Bottom), new Segment(r2.Top, r2.Bottom));

			if (!AreIntersected(r1, r2)) return 0;
			return Math.Abs(horizontalIntersection * verticallIntersection);
		}

		public static int GetIntersectionLength(Segment segment1, Segment segment2)
		{
			if (segment1.point1 <= segment2.point1 && segment1.point2 <= segment2.point2)
				return (segment1.point2 - segment2.point1);
			if (segment1.point1 <= segment2.point1 && segment1.point2 >= segment2.point2)
				return (segment2.point2 - segment2.point1);
			if (segment2.point1 <= segment1.point1 && segment2.point2 >= segment1.point2)
				return (segment1.point2 - segment1.point1);
			return (segment2.point2 - segment1.point1);
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
		public int point1;
		public int point2;

		public Segment(int point1, int point2)
		{
			this.point1 = point1;
			this.point2 = point2;
		}
	}

	public static class RectangleExtensions
	{
		public static bool IsInside(this Rectangle inner, Rectangle outer)
		{
			return (outer.Left <= inner.Left && outer.Right >= inner.Right) && (outer.Top <= inner.Top && outer.Bottom >= inner.Bottom);
		}
	}
}