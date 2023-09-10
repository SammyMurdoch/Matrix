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
        public void MatrixNotEqualTypeTest()
        {
            var m1 = new Matrix<int>(new int[,]
            {
                { 1, 2, 3},
                { 4, 5, 6},
                { 7, 8, 10}
            });

            var m2 = "hi";

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
        public void TransposeShapeTest()
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
        public void TransposeMirrorHorizontallyTest()
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
        public void MirrorHorizontallyTransposeTest()
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
        public void TransposeMirrorVerticallyTest()
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
        public void MirrorVerticallyTransposeTest()
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
        public void DoubleTransposeTest()
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
        public void MirrorVerticallyMirrorHorizontallyTest()
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
        public void Rotate90Test()
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
        public void Rotate180Test()
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
        public void Rotate270Test()
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
        public void Rotate271Test()
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

    [TestClass]
    public class MatrixTransformTrackerTests
    {
        [TestMethod]
        public void MHMVTTSimplificationTest()
        {
            var Tracker1 = new MatrixTransformTracker();

            Tracker1.AddTransform("MH");
            Console.WriteLine(Tracker1.ToString());

            Tracker1.AddTransform("MV");
            Console.WriteLine(Tracker1.ToString());

            Tracker1.AddTransform("T");
            Console.WriteLine(Tracker1.ToString());

            Tracker1.AddTransform ("T");
            Console.WriteLine(Tracker1.ToString());

            var Tracker2 = new MatrixTransformTracker(new List<string> { "MH", "MV" });

            Console.WriteLine("HI");
            Console.WriteLine(Tracker1.Transforms.ToString());
            Console.WriteLine(Tracker2.Transforms.ToString());

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void EqualTransformTrackerTest()
        {
            var Tracker1 = new MatrixTransformTracker(new List<string> { "MH", "MV" });
            var Tracker2 = new MatrixTransformTracker(new List<string> { "MH", "MV" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void CongruentTransformTrackerTest()
        {
            var Tracker1 = new MatrixTransformTracker(new List<string> { "MH", "MV" });
            var Tracker2 = new MatrixTransformTracker();

            Tracker2.AddTransform("MV");
            Tracker2.AddTransform("MH");

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void NotEqualTransformTrackerTest()
        {
            var Tracker1 = new MatrixTransformTracker(new List<string> { "MH", "T" });
            var Tracker2 = new MatrixTransformTracker(new List<string> { "MH", "MV" });

            Assert.AreNotEqual(Tracker1, Tracker2);
        }
    }
}