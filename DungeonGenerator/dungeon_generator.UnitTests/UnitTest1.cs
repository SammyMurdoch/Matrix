using dungeon_generator;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace dungeon_generator.UnitTests
{
    [TestClass]
    public class TreeNodeTests
    {
        [TestMethod]
        public void ParentIndexTest()
        {
            var Node = new TreeNode(2);

            Assert.AreEqual(Node.ParentIndex, 2);

        }

        [TestMethod]
        public void AddChildTest()
        {
            var Node = new TreeNode(0);

            Node.AddChild(1);
            Node.AddChild(2);

            Assert.IsTrue(Node.ChildIndices.SetEquals(new HashSet<int> { 2, 1 }));
        }

        [TestMethod]
        public void TreeNodeToStringTest()
        {
            var Node = new TreeNode(4);
            Node.AddChild(1);

            Assert.AreEqual(Node.ToString(), $"Parent: 4, Children: 1");
        }
    }

    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void TreeNoRootTest()
        {
            var Tree = new Tree();
            var Node0 = new TreeNode();
            var Node1 = new TreeNode(0);
            Tree.AddNode(Node0);
            Tree.AddNode(Node1);

            Assert.AreEqual(Tree.Root, Node0);
            Assert.AreEqual(Tree.Nodes[0], Node0);
            Assert.AreEqual(Tree.Nodes[1], Node1);
        }

        [TestMethod]
        public void TreeProvidedRootTest()
        {
            var RootNode = new TreeNode();
            var Tree = new Tree(RootNode);
            var Node1 = new TreeNode(0);

            Tree.AddNode(Node1);

            Assert.AreEqual(Tree.Root, RootNode);
            Assert.AreEqual(Tree.Nodes[0], RootNode);
            Assert.AreEqual(Tree.Nodes[1], Node1);
        }

        [TestMethod]
        public void AddNodeNoParentTest()
        {
            var Tree = new Tree(new TreeNode());

            Assert.ThrowsException<ArgumentException>(() => Tree.AddNode(new TreeNode()));
        }

        [TestMethod]
        public void AddNodeIncorrectParentTest()
        {
            var Tree = new Tree(new TreeNode(4));

            Assert.ThrowsException<ArgumentException>(() => Tree.AddNode(new TreeNode(2)));
        }
    }

    [TestClass]
    public class PartitionNodeTests
    {
        [TestMethod]
        public void PartitionNodeLenTests()
        {
            var PartitionNode = new PartitionNode(new int[][] { new int[2] { 0, 0 }, new int[2] { 2, 1 } });

            Assert.IsTrue(PartitionNode.XLen == 2);
            Assert.IsTrue(PartitionNode.YLen == 1);
        }

        [TestMethod]
        public void PartitionNodeCornerTests()
        {
            var PartitionNode = new PartitionNode(new int[][] { new int[2] { -1, 0 }, new int[2] { 2, 1 } });

            Assert.IsTrue(PartitionNode.TL.SequenceEqual(new int[] { -1, 1}));
            Assert.IsTrue(PartitionNode.TR.SequenceEqual(new int[] { 2, 1 }));
            Assert.IsTrue(PartitionNode.BL.SequenceEqual(new int[] { -1, 0 }));
            Assert.IsTrue(PartitionNode.BR.SequenceEqual(new int[] { 2, 0 }));
        }

        [TestMethod]

        public void PartitionNodeAreaTest()
        {
            var PartitionNode = new PartitionNode(new int[][] { new int[2] { -1, 0 }, new int[2] { 2, 1 } });

            Assert.IsTrue(PartitionNode.Area == 3);
        }

        [TestMethod]

        public void PartitionNodeDivisibilityTest()
        {
            var PartitionNode1 = new PartitionNode(new int[][] { new int[2] { -1, 0 }, new int[2] { 2, 1 } });
            var PartitionNode2 = new PartitionNode(new int[][] { new int[2] { -1, 0 }, new int[2] { 4, 1 } });
            var PartitionNode3 = new PartitionNode(new int[][] { new int[2] { -1, 0 }, new int[2] { 4, 5 } });

            Assert.IsTrue(PartitionNode1.IsIndivisible);
            Assert.IsTrue(PartitionNode2.IsIndivisible);
            Assert.IsFalse(PartitionNode3.IsIndivisible);
        }


    }
}