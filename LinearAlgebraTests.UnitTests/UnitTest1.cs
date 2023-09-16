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
            Assert.AreEqual(ArbitraryMatrix, ArbitraryMatrix);

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
            Assert.AreNotEqual(ArbitraryMatrix, "hi");
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
            var m = ArbitraryMatrix;
            var originalRows = m.Shape.rows;
            var originalCols = m.Shape.cols;

            m.Transpose();

            Assert.AreEqual(m.Shape, new Shape(originalCols, originalRows));
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
            var m1 = ArbitraryMatrix;

            m1.Transpose();
            m1.Transpose();

            var m2 = ArbitraryMatrix;

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
            Assert.ThrowsException<ArgumentException>(() => ArbitraryMatrix.Rotate(271));

        }

        private Matrix<int> ArbitraryMatrix 
        {
            get
            {
                return new Matrix<int>(new int[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 },
                    { 10, 11, 12 }
                });
            }
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
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform ("T");

            var Tracker2 = new MatrixTransformTracker(new List<string> { "MH", "MV" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void TMVMHTSimplificationTest()
        {
            var Tracker1 = new MatrixTransformTracker();

            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("T");

            var Tracker2 = new MatrixTransformTracker(new List<string> { "MV", "MH" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void TMVMHMVSimplificationTest()
        {
            var Tracker1 = new MatrixTransformTracker();

            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MV");

            var Tracker2 = new MatrixTransformTracker(new List<string> { "T", "MH" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void LongSimplificationTest()
        {
            var Tracker1 = new MatrixTransformTracker();

            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MH");

            var Tracker2 = new MatrixTransformTracker(new List<string> { "T", "MH" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void LongSimplificationTest2()
        {
            var Tracker1 = new MatrixTransformTracker();

            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MV");
            Tracker1.AddTransform("MH");
            Tracker1.AddTransform("T");
            Tracker1.AddTransform("MH");

            var Tracker2 = new MatrixTransformTracker(new List<string> { "MH", "MV" });

            Assert.AreEqual(Tracker1, Tracker2);
        }

        [TestMethod]
        public void SimplificationPathTest()
        {
            var PossibleTransforms = new List<string> { "T", "MH", "MV" };
            var Tracker = new MatrixTransformTracker();

            Random rnd = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                int RandomIndex = rnd.Next(PossibleTransforms.Count);
                Tracker.AddTransform(PossibleTransforms[RandomIndex]);
            }

            Assert.IsTrue(Tracker.Transforms.Count <= 4);
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