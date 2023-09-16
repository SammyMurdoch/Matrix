using System;
using System.Numerics;

namespace dungeon_generator
{
    public class TreeNode
    {
        public int Index { get; set; }
        public int? ParentIndex { get; set; }
        public HashSet<int> ChildIndices { get; set; }

        public TreeNode(int? ParentIndex = null)
        {
            this.ChildIndices = new HashSet<int>();
            this.ParentIndex = ParentIndex;
        }

        public override string ToString()
        {
            return $"Parent: {this.ParentIndex}, Children: {String.Join(", ", this.ChildIndices)}";
        }

        public void AddChild(int Child)
        {
            ChildIndices.Add(Child);
        }
    }
    public class Tree
    {
        public Dictionary<int, TreeNode> Nodes { get; set; }
        public TreeNode? Root { get; set; }

        public Tree(TreeNode? TreeRoot = null)
        {
            Nodes = new Dictionary<int, TreeNode>();

            if (TreeRoot != null)
            {
                this.AddNode(TreeRoot);
            }
        }

        public void AddNode(TreeNode NewNode)
        {
            NewNode.Index = Nodes.Count;

            if (NewNode.ParentIndex is null)
            {
                if (this.Root is null)
                {
                    Nodes.Add(NewNode.Index, NewNode);
                    this.Root = NewNode;
                }
                else
                {
                    throw new ArgumentException("Node must have a parent to add it to the tree.");
                }
            }
            else
            {
                if (this.Nodes.ContainsKey((int) NewNode.ParentIndex))
                {
                    this.Nodes.Add(NewNode.Index, NewNode);
                    this.Nodes[(int) NewNode.ParentIndex].AddChild(NewNode.Index);
                }
                else
                {
                    throw new ArgumentException("Parent not in the tree.");
                }
            }
        }
    }

    public class PartitionNode : TreeNode
    {
        public int Level { get; set; }
        public int[][] Bounds { get; set; }
        public int[][]? Room { get; set; }

        public PartitionNode(int[][] Bounds, int[][]? Room = null, int? Parent = null) : base(Parent)
        {
            this.Bounds = Bounds;
            this.Room = Room;
        }

        public int XLen
        {
            get { return Math.Abs(this.Bounds[1][0] - this.Bounds[0][0]); }
        }

        public int YLen
        {
            get { return Math.Abs(this.Bounds[1][1] - this.Bounds[0][1]); }
        }


        public int[] TL
        {
            get { return new int[] { this.Bounds[0][0], this.Bounds[1][1] }; }
        }
        public int[] TR
        {
            get { return this.Bounds[1]; }
        }
        public int[] BL
        {
            get { return this.Bounds[0]; }
        }
        public int[] BR
        {
            get { return new int[] { this.Bounds[1][0], this.Bounds[0][1] }; }
        }
        public int Area
        {
            get { return this.XLen * this.YLen; }
        }

        public bool IsIndivisible
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

        public override string ToString()
        {
            return $"Bounds: {this.Bounds}, Room: {this.Room}, {base.ToString}, Level: {this.Level}";
        }

        public bool CheckPartitionDimensions()
        {
            if (this.XLen >= 10 && this.YLen >= 10)
            {
                return true;
            }
            return false;
        }

        public int GetSplitAxis()
        {
            return 1;
            //Some sort of sampling required
        }

    }

    //public class Rectangle
    //{
    //    public HashSet<Vector2> Corners { get; set; }
    //    public Vector2 Center { get; set; }

    //    public Rectangle(HashSet<Vector2> Corners, Vector2? Center = null)
    //    {
    //        this.Corners = new HashSet<Vector2>();
    //        //Verify Corners
    //        this.Corners.UnionWith(Corners);


    //        if (Corners.Count == 1 !& Center is null)
    //        {
    //            var OtherCorners = new HashSet<Vector2>();
    //            this.Corners.Add()
    //        }
    //    }
    //}

    //public class Rectangle
    //{
    //    string[] ValidPoints = { "TopLeft", "TopRight", "BottomLeft", "BottomRight", "Center" };
    //    HashSet<string>[] ValidCornerCombinations = { new HashSet<string> { "TopLeft", "BottomRight" }, new HashSet<string> { "TopRight", "BottomLeft" } };

    //    public Vector2 TopRight { get; set; }
    //    public Vector2 TopLeft { get; set; }
    //    public Vector2 BottomLeft { get; set; }
    //    public Vector2 BottomRight { get; set; }
    //    public Vector2 Center { get; set; }

    //    HashSet<string> GetRectangleConstructorPoints(List<string> PointLabels)
    //    {
    //        var PointTypeSet = new HashSet<string>(PointLabels);

    //        if (PointLabels.Count < 2)
    //        {
    //            throw new ArgumentException("You must provide at least two points to define a Rectangle.");
    //        }

    //        foreach (var key in PointLabels)
    //        {
    //            if (!ValidPoints.Contains(key))
    //            {
    //                throw new ArgumentException($"Invalid point name: {key}.");
    //            }
    //        }

    //        if (PointLabels.Contains("Center"))
    //        {
    //            PointTypeSet.Remove("Center");
    //            var PointTypeList = new List<string>(PointTypeSet);

    //            return new HashSet<string> { "Center", PointTypeList[0] };
    //        }

    //        foreach (var CornerCombination in ValidCornerCombinations)
    //        {
    //            if (PointTypeSet.IsSupersetOf(CornerCombination))
    //            {
    //                return CornerCombination;
    //            }
    //        }

    //        throw new ArgumentException("The points provided are insufficient to describe a rectangle.");

    //    }

    //    public Vector2 GetOppositeCorner(Vector2 Center, Vector2 Corner)
    //    {
    //        return Center * 2 - Corner;
    //    }

    //    public Rectangle(Dictionary<string, Vector2> Points)
    //    {
    //        var PointKeyList = new List<string>(Points.Keys);
    //        var RectangleConstructorPoints = GetRectangleConstructorPoints(PointKeyList);

    //        if (RectangleConstructorPoints.SetEquals(new HashSet<string> { "TopRight", "BottomLeft" }))
    //        {
    //            this.TopRight = Points["TopRight"];
    //            this.BottomLeft = Points["BottomLeft"];
    //            this.TopLeft = new Vector2(Points["BottomLeft"][0], Points["TopRight"][1]);
    //            this.BottomRight = new Vector2(Points["TopRight"][0], Points["BottomLeft"][1]);
    //            this.Center =
    //        }
    //        else if (RectangleConstructorPoints.SetEquals(new HashSet<string> { "TopLeft", "BottomRight" }))
    //        {
    //            this.TopLeft = Points["TopLeft"];
    //            this.BottomRight = Points["BottomRight"];
    //            this.TopRight = new Vector2(Points["BottomRight"][0], Points["TopLeft"][1]);
    //            this.BottomLeft = new Vector2(Points["TopLeft"][0], Points["BottomRight"][1]);
    //        }
    //        else
    //        {
    //            if ()
    //        }


    //    }
    //}
}
