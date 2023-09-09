using System;
using System.Diagnostics.CodeAnalysis;

namespace linear_algebra 
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Matrix<int>(new int[,] {
                { 1, 2, 3},
                { 4, 5, 6}
            });
            AssertTrue(m[0, 0] == 1);
            AssertTrue(m[1, 2] == 6);

            var s1 = new Shape(3, 7);
            var s2 = new Shape(3, 7);

            AssertEqual(s1, s1);
            AssertEqual(s1, s2);

            AssertEqual(new Shape(2, 3), m.Shape);

            Console.WriteLine(m.ToString());
            Console.WriteLine();

            m.MirrorHorizontally();
            m.Transpose();

            AssertEqual(new Shape(3, 2), m.Shape);

            var t = m.GetTransformedIndex(0, 0);
            int[] expected = {1, 0};
            AssertArraysEqual(expected, t);

            AssertTrue(m[0, 0] == 4);

            Console.WriteLine(m.ToString());
            Console.WriteLine();

            var m2 = new Matrix<int>(new int[,] {
                { 1, 2, 3},
                { 4, 5, 6}
            });

            Console.WriteLine(m2.ToString());
            Console.WriteLine();

            m2.Transpose();
            m2.MirrorHorizontally();

            Console.WriteLine(m2.ToString());
        }

        static void AssertTrue(bool condition, string? message = null)
        {
            if (!condition)
            {
                var failureMessage = "Assertion Failed";
                if (message != null)
                {
                    failureMessage += ": ";
                    failureMessage += message;
                }

                throw new Exception(failureMessage);
            }
        }

        static void AssertArraysEqual(int[] expected, int[] actual)
        {
            for (int i = 0; i < expected.Length; i++)
            {
                var message = ArrayToString(actual) + " was not " + ArrayToString(expected);
                AssertTrue(expected[i] == actual[i], message);
            }
        }

        static void AssertEqual(object expected, object actual)
        {
            var message = expected.ToString() + " was not " + actual.ToString();
            AssertTrue(expected.Equals(actual), message);
        }

        static string ArrayToString(int[] array)
        {
            return "[" + string.Join(", ", array) + "]";
        }
    }

    public class Shape
    {
        public int rows { get; set; }
        public int cols { get; set; }

        public Shape(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
        }
        public void Transpose()
        {
            var temp = this.rows;
            this.rows = this.cols;
            this.cols = temp;
        }
        public override bool Equals(object? other)
        {
            if (other is Shape otherShape)
            {
                if (rows == otherShape.rows && cols == otherShape.cols)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"({rows}, {cols})";
        }

        public override int GetHashCode()
        {
            return rows.GetHashCode() + cols.GetHashCode();
        }
    }

    public interface MatrixTransform
    {
        public int[] TransformIndices(int[] indices, Shape shape);

        public Shape TransformShape(Shape shape);
    }

    public abstract class ShapeInvariantTransform: MatrixTransform
    {
        public abstract int[] TransformIndices(int[] indices, Shape shape);

        public virtual Shape TransformShape(Shape shape)
        {
            return shape;
        }
    }

    public class TransposeTransform: MatrixTransform
    {
        public int[] TransformIndices(int[] indices, Shape shape)
        {
            return new int[] { indices[1], indices[0] };
        }

        public Shape TransformShape(Shape shape)
        {
            return new Shape(shape.cols, shape.rows);
        }
    }

    public class MirrorHorizontallyTransform: ShapeInvariantTransform
    {
        public override int[] TransformIndices(int[] indices, Shape shape)
        {
            return new int[] { shape.rows-1-indices[0], indices[1] };
        }
    }

    public class MirrorVerticallyTransform: ShapeInvariantTransform
    {
        public override int[] TransformIndices(int[] indices, Shape shape)
        {
            return new int[] { indices[0], shape.cols-1-indices[1] };
        }
    }

    public class MatrixTransformTracker
    {
        private List<string> transforms;

        private Dictionary<string, MatrixTransform> transformTypes = new Dictionary<string, MatrixTransform>() {
            { "T", new TransposeTransform() },
            { "MV", new MirrorVerticallyTransform() },
            { "MH", new MirrorHorizontallyTransform() }
        };

        private Dictionary<List<string>, List<string>> simplifiedTransforms = new Dictionary<List<string>, List<string>>() {
            { new List<string> { "T", "T" }, new List<string> { } },
            { new List<string> { "MH", "MH" }, new List<string> { } },
            { new List<string> { "MV", "MV" }, new List<string> { } },

            { new List<string> { "T", "MV", "T" }, new List<string> { "MH" } },
            { new List<string> { "T", "MH", "T" }, new List<string> { "MV" } },
            { new List<string> { "T", "MH", "MV" }, new List<string> { "T", "MV", "MH" } },

            { new List<string> { "MV", "T", "MV" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MV", "T", "MH" }, new List<string> { "T" } },
            { new List<string> { "MV", "MH", "T" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MV", "MH", "MV" }, new List<string> { "MH" } },

            { new List<string> { "MH", "T", "MV" }, new List<string> { "T" } },
            { new List<string> { "MH", "T", "MH" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MH", "MV", "T" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MH", "MV", "MH" }, new List<string> { "MV" } },

            { new List<string> { "T", "MV", "MH", "T" }, new List<string> { "MH", "MV" } },
            { new List<string> { "T", "MV", "MH", "MV" }, new List<string> { "T", "MH" } }
        };

        public MatrixTransformTracker()
        {
            this.transforms = new List<string>();
        }

        public void AddTransform(string transform)
        {
            this.transforms.Add(transform);

            List<string> value;
            if (simplifiedTransforms.TryGetValue(this.transforms, out value))
            {
                this.transforms = value;
            }
        }

        public int[] TransformIndices(int row, int col, Shape shape) // does it keep track of shape changes during the transformation process?
        {
            int[] transformedIndices = {row, col};
            Shape transformedShape = shape;

            foreach (var transform in transforms.AsEnumerable().Reverse())
            {
                transformedShape = transformTypes[transform].TransformShape(transformedShape);
                transformedIndices = transformTypes[transform].TransformIndices(transformedIndices, transformedShape);

            }

            return transformedIndices;
        }

        public Shape TransformShape(Shape shape)
        {
            var transformedShape = shape;

            foreach (var transform in transforms)
            {
                transformedShape = transformTypes[transform].TransformShape(transformedShape);
            }

            return transformedShape;
        }
    }

    public class Matrix<T>
    {
        private T[,] values;
        private Shape shape;
        public MatrixTransformTracker transformations;

        public Matrix(Shape shape) : this(new T[shape.rows, shape.cols]) { }

        public Matrix(T[,] values)
        {
            this.values = values;
            this.shape = new Shape(values.GetLength(0), values.GetLength(1));
            this.transformations = new MatrixTransformTracker();
        }

        public Shape Shape
        {
            get
            {
                return transformations.TransformShape(this.shape);

            }
        }

        public void Transpose()
        {
            transformations.AddTransform("T");
        }

        public void MirrorHorizontally()
        {
            transformations.AddTransform("MH");
        }

        public void MirrorVertically()
        {
            transformations.AddTransform("MV");
        }

        public override string ToString() 
        {
            string matrixString = String.Empty;

            for (int i = 0; i < Shape.rows; i++)
            {
                for (int j = 0; j < Shape.cols; j++)
                {
                    matrixString += this[i, j].ToString();
                }
                matrixString += "\n";
            }

            return matrixString[..^1];
        }

        public virtual T this[int row, int col]
        {
            get
            {
                int[] indices = GetTransformedIndex(row, col);
                return this.values[indices[0], indices[1]];
            }
            set
            {
                int[] indices = GetTransformedIndex(row, col);
                this.values[indices[0], indices[1]] = value;
            }
        }

        public int[] GetTransformedIndex(int row, int col)
        {
            return transformations.TransformIndices(row, col, this.Shape);
        }

        public override bool Equals(object? other)
        {
            if (other is Matrix<T> otherMatrix)
            {
                if (!Equals(this.Shape, otherMatrix.Shape))
                {
                    Console.WriteLine($"{this.Shape  == otherMatrix.Shape}");
                    return false;
                }
                for (int i = 0; i < Shape.rows; i++)
                {
                    for (int j = 0; j < Shape.cols; j++)
                    {
                        if (!EqualityComparer<T>.Default.Equals(this[i, j], otherMatrix[i, j]))
                        {
                            return false;
                        } 
                    }
                }
            }

            return true;
            
        }
    }
}
