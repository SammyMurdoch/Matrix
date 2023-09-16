using System;

namespace dungeon_generator
{
    public class TreeNode
    {
        public int Index { get; set; }
        public int ParentIndex { get; set; }
        public HashSet<int> ChildIndices { get; set; }

        public TreeNode(int? ParentIndex)
        {
            this.ParentIndex = ParentIndex;
        }

        public override ToString()
        {
            return $"Parent: {this.ParentIndex}, Children: {this.ChildIndices}"
        }

        public void AddChild(int Child)
        {
            ChildIndices.Add(Child);
        }

    }
    public class Tree
    {
        public Dictionary<int, TreeNode> Nodes { get; set; }
        public TreeNode Root { get; set; }

        public Tree(TreeNode Root = null)
        {
            new Dictionary<int, TreeNode>();

            if (Root != null)
            {
                this.Root = Root;
            }
        }

        public Tree()
        {
            this.Root = null;
        }

        public AddNode(TreeNode NewNode)
        {
            NewNode.Index = Nodes.Count;

            if (NewNode.ParentIndex is null)
            {
                if (this.Root is null)
                {
                    Nodes.Add(NewNode.Index, NewNode);
                    this.Root = NewNode.Index;
                }
                else
                {
                    throw ArgumentException("Node must have a parent to add it to the tree.");
                }
            else
                {
                    if (this.Nodes.ContainsKey(NewNode.ParentIndex))
                    {
                        this.Nodes.Add(NewNode.Index, NewNode);
                        this.Nodes[NewNode.ParentIndex].AddChild(NewNode.Index);
                    }
                    else
                    {
                        throw ArgumentException("Parent not in the tree.");
                    }
                }
            }
        }
    }

    public class PartitionNode : TreeNode
    {
        public int Level { get; set; }
        public int[,] Bounds { get; set; }
        public int[,] Room { get; set; }

        public int XLen
        {
            get return Abs(this.Bounds[1, 0] - this.Bounds[0, 0]); }

        public int YLen
        {
            get return Abs(this.Boudns[1, 1] - this.Bounds[0, 1]); }


        public int TL
        {
            get return new int[] { this.Bounds[0, 0], this.Bounds[1, 1] }; }
        public int TR
        {
            get return this.Bounds[1]; }
        public int BL
        {
            get return this.Bounds[0]; }
        public int BR { get return new int[] { this.Bounds[1, 0], this.Bounds[0, 1]; } }
        public int Area
        {
            get return this.XLen * this.YLen; }

        public bool IsDivisible
        {
            get
            {
                if (this.XLen < 5 || this.YLen < 5)
                {
                    return true;
                }
                return false;
            }
        }

        public PartitionNode(int[,] Bounds, int[,] Room = null, int Parent = null) : base(Parent)
        {
            this.Bounds = Bounds;
            this.Room = Room;
        }

        public override ToString()
        {
            return $"Bounds: {this.Bounds}, Room: {this.Room}, {base.ToString}, Level: {this.Level}";
        }

        public bool CheckPartitionDimensions()
        {
            if (this.XLen >= 10 && self.YLen >= 10)
            {
                return true;
            }
            return false;
        }

        public int GetSplitAxis()
        {
            //Some sort of sampling required
        }

    }
}
