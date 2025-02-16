using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.View.Animations
{
    public class CardCollisionChecker
    {
        private readonly float width;  // Szerokość prostokąta
        private readonly float height; // Wysokość prostokąta

        /// <summary>
        /// Tworzy nowy obiekt sprawdzający kolizje prostokątów o zadanych wymiarach.
        /// </summary>
        public CardCollisionChecker(double width, double height)
        {
            this.width = (float)width;
            this.height = (float)height;
        }

        /// <summary>
        /// Sprawdza, czy dwa prostokąty nachodzą na siebie.
        /// Pierwszy prostokąt nie jest obrócony, drugi może być obrócony o dowolny kąt.
        /// </summary>
        public bool AreRectanglesOverlapping(Vector2 rect1Center, Vector2 rect2Center, double angle2)
        {
            // Granice pierwszego prostokąta (AABB)
            float x1Min = rect1Center.X - width / 2, x1Max = rect1Center.X + width / 2;
            float y1Min = rect1Center.Y - height / 2, y1Max = rect1Center.Y + height / 2;

            // Pobieramy wierzchołki drugiego prostokąta po obrocie
            Vector2[] rect2 = GetRotatedRectangleVertices(rect2Center, (float)angle2);

            // Sprawdzamy Separating Axis Theorem (SAT) dla AABB oraz drugiego prostokąta
            return !HasSeparatingAxisAABB(rect2, x1Min, x1Max, y1Min, y1Max) && !HasSeparatingAxis(rect2);
        }

        private Vector2[] GetRotatedRectangleVertices(Vector2 center, float angle)
        {
            float rad = MathF.PI * angle / 180.0f;
            float cosA = MathF.Cos(rad);
            float sinA = MathF.Sin(rad);

            // Wierzchołki przed obrotem (lokalne współrzędne)
            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(-width / 2, -height / 2), // Lewy górny
                new Vector2(width / 2, -height / 2),  // Prawy górny
                new Vector2(width / 2, height / 2),   // Prawy dolny
                new Vector2(-width / 2, height / 2)   // Lewy dolny
            };

            // Obracamy i przesuwamy do środka prostokąta
            for (int i = 0; i < 4; i++)
            {
                float xNew = vertices[i].X * cosA - vertices[i].Y * sinA;
                float yNew = vertices[i].X * sinA + vertices[i].Y * cosA;
                vertices[i] = new Vector2(xNew, yNew) + center;
            }

            return vertices;
        }

        private bool HasSeparatingAxisAABB(Vector2[] rect2, float x1Min, float x1Max, float y1Min, float y1Max)
        {
            // Sprawdzamy czy drugi prostokąt leży całkowicie poza granicami AABB (pierwszego prostokąta)
            float minX2 = float.MaxValue, maxX2 = float.MinValue;
            float minY2 = float.MaxValue, maxY2 = float.MinValue;

            foreach (var v in rect2)
            {
                minX2 = MathF.Min(minX2, v.X);
                maxX2 = MathF.Max(maxX2, v.X);
                minY2 = MathF.Min(minY2, v.Y);
                maxY2 = MathF.Max(maxY2, v.Y);
            }

            return maxX2 < x1Min || minX2 > x1Max || maxY2 < y1Min || minY2 > y1Max;
        }

        private bool HasSeparatingAxis(Vector2[] rect2)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 edge = rect2[(i + 1) % 4] - rect2[i];
                Vector2 axis = new Vector2(-edge.Y, edge.X); // Normalna do krawędzi
                axis = Vector2.Normalize(axis);

                float min2 = float.MaxValue, max2 = float.MinValue;

                foreach (var v in rect2)
                {
                    float projection = Vector2.Dot(v, axis);
                    min2 = MathF.Min(min2, projection);
                    max2 = MathF.Max(max2, projection);
                }

                // Jeśli przedziały się nie pokrywają - brak kolizji
                if (max2 < 0 || min2 > 0) // 0 oznacza tutaj AABB w pozycji (0,0)
                    return true;
            }

            return false;
        }
    }
}
