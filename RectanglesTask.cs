using System;

namespace Rectangles
{

	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			var horizontalNoIntersection =
				r1.intoSegment(Side.Horizontal)
				.NoIntersection
				(r2.intoSegment(Side.Horizontal));

			var verticallNoIntersection =
				r1.intoSegment(Side.Vertical)
				.NoIntersection
				(r2.intoSegment(Side.Vertical));
			return !(horizontalNoIntersection || verticallNoIntersection);
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			var horizontalIntersection =
				r1.intoSegment(Side.Horizontal)
				.GetIntersectionLength
				(r2.intoSegment(Side.Horizontal));

			var verticallIntersection =
				r1.intoSegment(Side.Vertical)
				.GetIntersectionLength
				(r2.intoSegment(Side.Vertical));

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

	/// <summary>
	/// Дополнительные классы и методы расширения
	/// </summary>

	public enum Side
	{
		Horizontal,
		Vertical
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
			if (NoIntersection(segment1, segment2))
				return 0;
			int[] points = { segment1.Point1, segment1.Point2, segment2.Point1, segment2.Point2 };
			Array.Sort(points);
			return points[2] - points[1];
		}

		public static bool NoIntersection(this Segment segment1, Segment segment2)
		{
			return segment1.Point2 < segment2.Point1 || segment1.Point1 > segment2.Point2;
		}
	}

	public static class RectangleExtensions
	{
		/// <summary>
		/// Выдает координаты горзонтальной или вертикальной проекции прямоугольника  
		/// </summary>
		/// <param name="rX">Заданный прямоугольник</param> 
		/// <param name="side">Проекция для вычисления - горизонтальная или вертикальная</param>
		/// <returns>Координаты проекции</returns>
		public static Segment intoSegment(this Rectangle rX, Side side)
		{
			switch (side)
			{
				case Side.Horizontal:
					return new Segment(rX.Left, rX.Right);
			}
			return new Segment(rX.Top, rX.Bottom);
		}

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