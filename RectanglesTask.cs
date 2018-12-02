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
			int sideA = 0;
			int sideB = 0;
			//int horizontalIntersection = GetIntersectionLength(r1, r2);

			if (!AreIntersected(r1, r2)) return 0;

			if (r1.Left <= r2.Left && r1.Right <= r2.Right) sideA = (r1.Right - r2.Left);
			else if (r1.Left <= r2.Left && r1.Right >= r2.Right) sideA = (r2.Right - r2.Left);
			else if (r2.Left <= r1.Left && r2.Right >= r1.Right) sideA = (r1.Right - r1.Left);
			else sideA = (r2.Right - r1.Left);

			if (r1.Top <= r2.Top && r1.Bottom <= r2.Bottom) sideB = (r1.Bottom - r2.Top);
			else if (r1.Top <= r2.Top && r1.Bottom >= r2.Bottom) sideB = (r2.Bottom - r2.Top);
			else if (r2.Top <= r1.Top && r2.Bottom >= r1.Bottom) sideB = (r1.Bottom - r1.Top);
			else sideB = (r2.Bottom - r1.Top);

			return Math.Abs(sideA * sideB);
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

	public static class RectangleExtensions
	{
		public static bool IsInside(this Rectangle inner, Rectangle outer)
		{
			return (outer.Left <= inner.Left && outer.Right >= inner.Right) && (outer.Top <= inner.Top && outer.Bottom >= inner.Bottom);
		}
	}
}