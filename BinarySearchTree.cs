using System;
using SRTCls;
using System.Reflection;

public sealed class BinarySearchTree<T> : IBinarySearchTree<T>
{
    /// <summary>
    /// This is constructor which initializes duration,srtType of a Key Object.It uses reflection.
    /// </summary>
    /// <param name="duration">Total time of Video/Audio file</param>
    /// <param name="srtType">Type of Video/Audio file in terms of Time</param>
    public BinarySearchTree( double duration, int srtType)
    {
        Type t = typeof(T);
        FieldInfo setDuration = t.GetField("duration");
        setDuration.SetValue(t, duration);
        FieldInfo setSrtType = t.GetField("srtType");
        setSrtType.SetValue(t, srtType);
    }

    public TreeNode<T> Root { get; set; }

    //public BinarySearchTree()
    //{
    //    this.Root = null;
    //}

    //public void Remove(T x)
    //{
    //    this.Root = Remove(x, this.Root);
    //}

    //public void RemoveMin()
    //{
    //    this.Root = RemoveMin(this.Root);
    //}

    //public T FindMin()
    //{
    //    return ElementAt(FindMin(this.Root));
    //}

    //public T FindMax()
    //{
    //    return ElementAt(FindMax(this.Root));
    //}

    //public T Find(T x)
    //{
    //    return ElementAt(Find(x, this.Root));
    //}


    //public void MakeEmpty()
    //{
    //    this.Root = null;
    //}

    //public bool IsEmpty()
    //{
    //    return this.Root == null;
    //}

    public void Insert(T x)
    {
        this.Root = Insert(x, this.Root);
    }

    public T FindRange(T x)
    {
        return ElementAt(FindRange(x, this.Root));
    }

    public T ElementAt(TreeNode<T> t)
    {
        return t == null ? default(T) : t.Element;
    }

    //public TreeNode<T> Find(T x, TreeNode<T> t)
    //{
    //    while (t != null)
    //    {
    //        if ((x as IComparable).CompareTo(t.Element) < 0)
    //        {
    //            t = t.Left;
    //        }
    //        else if ((x as IComparable).CompareTo(t.Element) > 0)
    //        {
    //            t = t.Right;
    //        }
    //        else
    //        {
    //            return t;
    //        }
    //    }

    //    return null;
    //}


    /// <summary>
    /// This is search method to get dialogue from SRT with respect to searh object provided.In this case T x i.e. first argument.
    /// </summary>
    /// <param name="x">Object to be searched in the SRT</param>
    /// <param name="t">Collection of Binart Tree Nodes</param>
    public TreeNode<T> FindRange(T x, TreeNode<T> t)
    {

        //while current not is not null continue searching
        while (t != null)
        {
            //if CompareRange returns 3 then search the object to the left side of the binary tree.
            if ((x as ICompareRange).CompareRange(t.Element) == 3)
            {
                t = t.Left;
            }
            else if ((x as ICompareRange).CompareRange(t.Element) == 2)//if CompareRange returns 3 then search the object to the right side of the binary tree.
            {
                t = t.Right;
            }
            else if ((x as ICompareRange).CompareRange(t.Element) == 1)//if CompareRange returns 0 then search the object not found.
            {
                return t;
            }
            else
            {
                t = null;
                return t;
            }

            
        }

        return t;
    }

    /// <summary>
    /// This method is used to insert object into B Tree.
    /// </summary>
    /// <param name="x">Object to be inserted into B Tree</param>
    /// <param name="t">Collection of Binart Tree Nodes</param>
    public TreeNode<T> Insert(T x, TreeNode<T> t)
    {
        if (t == null)
        {
            t = new TreeNode<T>(x);
        }
        else if ((x as IComparable).CompareTo(t.Element) < 0)
        {
            t.Left = Insert(x, t.Left);
        }
        else if ((x as IComparable).CompareTo(t.Element) > 0)
        {
            t.Right = Insert(x, t.Right);
        }
        else
        {
            throw new Exception("Duplicate item");
        }
         
        return t;
    }

    //public TreeNode<T> RemoveMin(TreeNode<T> t)
    //{
    //    if (t == null)
    //    {
    //        throw new Exception("Item not found");
    //    }
    //    else if (t.Left != null)
    //    {
    //        t.Left = RemoveMin(t.Left);
    //        return t;
    //    }
    //    else
    //    {
    //        return t.Right;
    //    }
    //}

    //public TreeNode<T> Remove(T x, TreeNode<T> t)
    //{
    //    if (t == null)
    //    {
    //        throw new Exception("Item not found");
    //    }
    //    else if ((x as IComparable).CompareTo(t.Element) < 0)
    //    {
    //        t.Left = Remove(x, t.Left);
    //    }
    //    else if ((x as IComparable).CompareTo(t.Element) > 0)
    //    {
    //        t.Right = Remove(x, t.Right);
    //    }
    //    else if (t.Left != null && t.Right != null)
    //    {
    //        t.Element = FindMin(t.Right).Element;
    //        t.Right = RemoveMin(t.Right);
    //    }
    //    else
    //    {
    //        t = (t.Left != null) ? t.Left : t.Right;
    //    }

    //    return t;
    //}

    //public override string ToString()
    //{
    //    return this.Root.ToString();
    //}


 
}