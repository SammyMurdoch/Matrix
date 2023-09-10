using linear_algebra;
using System.ComponentModel.DataAnnotations;

namespace LinearAlgebraTests
{
    [TestClass]
    public class ShapeTests
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

            Assert.AreNotEqual(s4, s3);
        }
    }

    [TestClass]
    public class MatrixTests
    {
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

        [TestMethod]
        public void TransposeMirrorHorizontally()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            m1.Transpose();
            m1.MirrorHorizontally();

            var m2 = new Matrix<int>(new int[,]
             {
                { 3, 6, 9 },
                { 2, 5, 8 },
                { 1, 4, 7 }
             });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void MirrorHorizontallyTranspose()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            m1.MirrorHorizontally();
            m1.Transpose();

            var m2 = new Matrix<int>(new int[,]
             {
                { 7, 4, 1 },
                { 8, 5, 2 },
                { 9, 6, 3 }
             });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void TransposeMirrorVertically()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 }
             });

            m1.Transpose();
            m1.MirrorVertically();

            var m2 = new Matrix<int>(new int[,]
             {
                { 4, 1 },
                { 5, 2 },
                { 6, 3 }
             });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void MirrorVerticallyTranspose()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 }
             });

            m1.MirrorVertically();
            m1.Transpose();

            var m2 = new Matrix<int>(new int[,]
             {
                { 3, 6 },
                { 2, 5 },
                { 1, 4 }
             });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void DoubleTranspose()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 }
             });

            m1.Transpose();
            m1.Transpose();

            var m2 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 }
             });

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void MirrorVerticallyMirrorHorizontally()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            var m2 = new Matrix<int>(new int[,]
             {
                { 9, 8, 7 },
                { 6, 5, 4 },
                { 3, 2, 1 }
             });

            m1.MirrorHorizontally();
            m1.MirrorVertically();

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void Rotate90()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 }
             });

            var m2 = new Matrix<int>(new int[,]
             {
                { 4, 1 },
                { 5, 2 },
                { 6, 3 }
             });

            m1.Rotate(90);

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void Rotate180()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            var m2 = new Matrix<int>(new int[,]
             {
                { 9, 8, 7 },
                { 6, 5, 4 },
                { 3, 2, 1 }
             });

            m1.Rotate(180);

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void Rotate270()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            var m2 = new Matrix<int>(new int[,]
             {
                { 3, 6, 9 },
                { 2, 5, 8 },
                { 1, 4, 7 }
             });

            m1.Rotate(270);

            Assert.AreEqual(m1, m2);
        }

        [TestMethod]
        public void Rotate271()
        {
            var m1 = new Matrix<int>(new int[,]
             {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
             });

            Assert.ThrowsException<ArgumentException>(() => m1.Rotate(271));

        }
    }
}