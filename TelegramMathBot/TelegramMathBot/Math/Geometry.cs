using System;

namespace MathModule
{
    public class Vector3d
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d(params double[] coords)
        {
            if (coords.Length != 3) throw new ArgumentException();
            X = coords[0];
            Y = coords[1];
            Z = coords[2];
        }

        public static Vector3d operator -(Vector3d vect) => new Vector3d(-vect.X, -vect.Y, -vect.Z);

        public static Vector3d operator +(Vector3d first, Vector3d second) =>
            new Vector3d(first.X + second.X, first.Y + second.Y, first.Z + second.Z);

        public static Vector3d operator -(Vector3d first, Vector3d second) => first + (-second);

        public static Vector3d operator *(Vector3d vect, double value) =>
            new Vector3d(vect.X * value, vect.Y * value, vect.Z * value);

        public static Vector3d operator *(double value, Vector3d vect) => vect * value;

        public static double ScalarMul(Vector3d first, Vector3d second) =>
            first.X + second.X + first.Y * second.Y + first.Z * second.Z;

        public double Length() => Math.Sqrt(ScalarMul(this, this));

        public static Vector3d VectorMul(Vector3d first, Vector3d second)
        {
            var x = first.Y * second.Z - first.Z * second.Y;
            var y = first.Z * second.X - first.X * second.Z;
            var z = first.X * second.Y - first.Y * second.X;
            return new Vector3d(x, y, z);
        }

        public static double MixedMul(Vector3d first, Vector3d second, Vector3d third) =>
            ScalarMul(first, VectorMul(second, third));

        public static Vector3d Projection(Vector3d vect, Vector3d line)
        {
            if (line.Length() <= 10e-6) throw new ArithmeticException();
            return ScalarMul(vect, line) / ScalarMul(line, line) * line;
        }

        public Vector3d Normalised()
        {
            var length = Length();
            return new Vector3d(X / length, Y / length, Z / length);
        }

        public bool CollinearWith(Vector3d other)
        {
            if (Length() == 0 || other.Length() == 0) return true;
            return Math.Abs(Math.Abs(ScalarMul(this, other)) - 1) < 10e-6;
        }

        public double DistanceTo(Vector3d other) => (this - other).Length();
    }

    public class Segment
    {
        public Vector3d From { get; }
        public Vector3d To { get; }

        public Segment(Vector3d from, Vector3d to)
        {
            From = from;
            To = to;
        }

        public double Length() => (To - From).Length();

        public double DistanceTo(Vector3d point)
        {
            var relativeProjection = Vector3d.Projection(point - From, To - From);
            var relativeProjectionLength = relativeProjection.Length();
            if (relativeProjectionLength <= 0) return point.DistanceTo(From);
            if (relativeProjectionLength >= 1) return point.DistanceTo(To);
            return (point - From).DistanceTo(relativeProjection);
        }
        
        public double DistanceTo(Segment other)
        {
            var firstDirection = To - From;
            var secondDirection = other.To - other.From;
            var normal = Vector3d.VectorMul(firstDirection, secondDirection).Normalised();
            var distance = Vector3d.ScalarMul(From - other.From, normal);
            // shit fuck ne to we need, ne to.
            throw new NotImplementedException();
        }
    }

    public class Surface
    {
        public Vector3d PointOnSurface { get; }
        public Vector3d T1 { get; }
        public Vector3d T2 { get; }

        public Vector3d Normal { get; }

        public Surface(Vector3d pointOnSurface, Vector3d t1, Vector3d t2)
        {
            PointOnSurface = pointOnSurface;
            T1 = t1;
            T2 = t2;
            Normal = Vector3d.VectorMul(t1, t2).Normalised();
        }

        public bool Contains(Vector3d point) => Math.Abs(Vector3d.MixedMul(T1, T2, point - PointOnSurface)) < 10e-6;

        public Vector3d GetPointProjection(Vector3d point)
        {
            var relativePosition = point - PointOnSurface;
            return PointOnSurface + Vector3d.Projection(relativePosition, T1) +
                   Vector3d.Projection(relativePosition, T2);
        }

        public double DistanceTo(Vector3d point) => (point - GetPointProjection(point)).Length();

        public double DistanceTo(Segment segment)
        {
            var firstHeight = Vector3d.Projection(segment.From - PointOnSurface, Normal);
            var secondHeight = Vector3d.Projection(segment.To - PointOnSurface, Normal);
            var minOfHeight = Math.Min(firstHeight.Length(), secondHeight.Length());
            if ((firstHeight + secondHeight).Length() < minOfHeight)
                return 0;
            return minOfHeight;
        }

        public Tuple<Vector3d, bool> GetSegmentIntersection(Segment segment)
        {
            var lengthOfSegmentProjection = Vector3d.Projection(segment.From - segment.To, Normal).Length();
            var distanceToSegmentStart = DistanceTo(segment.From);
            if (Math.Abs(lengthOfSegmentProjection) < 10e-6 && Math.Abs(distanceToSegmentStart) > 10e-6)
                throw new ArithmeticException();
            var lambda = distanceToSegmentStart / lengthOfSegmentProjection;
            var pointOfIntersection = segment.From + (segment.To - segment.From) * lambda;
            var pointInsideSegment = lambda >= 0 && lambda <= 1;
            return new Tuple<Vector3d, bool>(pointOfIntersection, pointInsideSegment);
        }

        public Segment GetSegmentProjection(Segment segment) =>
            new Segment(GetPointProjection(segment.From), GetPointProjection(segment.To));

        public bool IntersectsWith(Surface other) => !Normal.CollinearWith(other.Normal);
    }
}