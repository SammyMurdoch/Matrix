using linear_algebra;
using System.ComponentModel.DataAnnotations;

namespace MatrixTests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void ShapeEqualTest()
        {
            var s1 = new Shape(2, 3);
            var s2 = new Shape(2, 3);

            Assert.AreEqual(s1, s2);
        }

        [TestMethod] 
        public void ShapeNotEqualTest()
        {
            var s3 = new Shape(2, 3);
            var s4 = new Shape(3, 4);

            Assert.AreNotEqual(s4 , s3);
        }

        [TestMethod]
        public void MatrixEqualTest()
        {
            var m1 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            });

            var m2 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            });

            Assert.AreEqual(m1, m2);

        }

        [TestMethod]
        public void MatrixNotEqualTest()
        {
            var m1 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 10}
            });

            var m2 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            });

            Assert.AreNotEqual(m1, m2);

        }

        [TestMethod]
        public void MatrixNotEqualShapeTest()
        {
            var m1 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6}
            });

            var m2 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 9}
            });

            Assert.AreNotEqual(m1, m2);

        }

        [TestMethod]
        public void MatrixEntryTest()
        {
            var m = new Matrix<int>(new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            Assert.AreEqual(6, m[1, 2]);
            Assert.AreEqual(7, m[2, 0]);
        }

        [TestMethod]
        public void TransposeTest_Shape()
        {
            var m = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6}
            });

            m.Transpose();

            Assert.AreEqual(m.Shape, new Shape(3, 2));
        }
    }
}