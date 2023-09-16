using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace linear_algebra 
{
    class Program
    {
        static void Main(string[] args)
        {
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

    interface IMatrixTransform
    {
        public int[] TransformIndices(int[] indices, Shape shape);

        public Shape TransformShape(Shape shape);
    }

    public abstract class ShapeInvariantTransform: IMatrixTransform
    {
        public abstract int[] TransformIndices(int[] indices, Shape shape);

        public virtual Shape TransformShape(Shape shape)
        {
            return shape;
        }
    }

    public class TransposeTransform: IMatrixTransform
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

        public List<string> Transforms { get { return transforms; } }

        private Dictionary<string, IMatrixTransform> transformTypes = new Dictionary<string, IMatrixTransform>() {
            { "T", new TransposeTransform() },
            { "MV", new MirrorVerticallyTransform() },
            { "MH", new MirrorHorizontallyTransform() }
        };

        private Dictionary<List<string>, List<string>> simplifiedTransforms = new Dictionary<List<string>, List<string>>(new ListComparer<string>()) {
            { new List<string> { "T", "T" }, new List<string> { } },
            { new List<string> { "MH", "MH" }, new List<string> { } },
            { new List<string> { "MV", "MV" }, new List<string> { } },
            { new List<string> { "MH", "MV" }, new List<string> { "MV", "MH"} },

            { new List<string> { "T", "MV", "T" }, new List<string> { "MH" } },
            { new List<string> { "T", "MH", "T" }, new List<string> { "MV" } },

            { new List<string> { "MV", "T", "MV" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MV", "T", "MH" }, new List<string> { "T" } },
            { new List<string> { "MV", "MH", "T" }, new List<string> { "T", "MV", "MH" } },
            { new List<string> { "MV", "MH", "MV" }, new List<string> { "MH" } },

            { new List<string> { "MH", "T", "MV" }, new List<string> { "T" } },
            { new List<string> { "MH", "T", "MH" }, new List<string> { "T", "MV", "MH" } },

            { new List<string> { "T", "MV", "MH", "T" }, new List<string> { "MV", "MH" } }
        };

        public override string ToString()
        {
            return String.Join(", ", this.transforms);
        }

        public MatrixTransformTracker()
        {
            this.transforms = new List<string>();
        }

        public MatrixTransformTracker(List<string> transforms)
        {
            this.transforms = new List<string>();

            foreach (string transform in transforms)
            {
                if (!transformTypes.ContainsKey(transform))
                {
                    throw new ArgumentException("Invalid transform code.");
                }

                this.AddTransform(transform);
            }
        }

        public void AddTransform(string transform)
        {
            bool TransformListUpdated = false;

            this.transforms.Add(transform);

            do
            {
                for (int i = 1; i < this.transforms.Count; i++)
                {
                    List<string> SimplifiedValue;

                    if (simplifiedTransforms.TryGetValue(this.transforms.GetRange(i - 1, this.transforms.Count - i + 1), out SimplifiedValue))
                    {
                        this.transforms.RemoveRange(i - 1, this.transforms.Count - i + 1);
                        this.transforms.AddRange(SimplifiedValue);
                        TransformListUpdated = true;
                        break;
                    }
                    TransformListUpdated = false;
                }
            } while (TransformListUpdated && this.transforms.Count >= 2);


        }

        public int[] TransformIndices(int row, int col, Shape shape)
        {
            int[] transformedIndices = { row, col };
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

        public override bool Equals(object? other)
        {
            if (other is MatrixTransformTracker otherTransformTracker)
            {
                if (this.transforms.SequenceEqual(otherTransformTracker.transforms))
                {
                    return true;
                }
            }
            return false;
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

        public void Rotate(int degrees)
        {
            int[] ValidRotations = { 0, 90, 180, 270, 360 };

            if (!ValidRotations.Contains(degrees))
            {
                throw new ArgumentException("Invalid rotation, only 0, 90, 180, 360 are valid.");
            }
            else if (degrees == 90)
            {
                Transpose();
                MirrorVertically();
            }
            else if (degrees == 180)
            {
                MirrorHorizontally();
                MirrorVertically();
            }
            else if (degrees == 270)
            {
                MirrorVertically();
                Transpose();
            }
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
                return true;
            }
            return false;  
        }

        //public override int GetHashCode()
        //{
        //    return rows.GetHashCode() + cols.GetHashCode();
        //}
    }

    public class ListComparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> x, List<T> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<T> obj)
        {
            int hashcode = 0;
            foreach (T t in obj)
            {
                hashcode ^= t.GetHashCode();
            }
            return hashcode;
        }
    }
}