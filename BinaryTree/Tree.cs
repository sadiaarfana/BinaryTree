using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Node
    {
        public Node(int data)
        {
            ID = data;
            IsBall = false;
        }
        public int ID;
        public bool IsBall;
        public Node Left;
        public Node Right;
    }

    class Tree
    {
        public Node root { get; }
        public Tree(int data)
        {
            root = buildTree(data, 1);
        }

        private Node buildTree(int data, int i)
        {
            if (i > data) return null;
            Node next = new Node(i);
            next.Left = buildTree(data, 2 * i);
            next.Right = buildTree(data, 2 * i + 1);
            return next;
        }

        public void traverse(Node root, ref Dictionary<int, bool> dictionary, ref int index)
        {
            if (root == null) return;

            // If traverse reach at last node then ball will be in that container
            if (root.Left == null && root.Right == null)
            {
                root.IsBall = true;
                if (dictionary.ContainsKey(root.ID)) dictionary[root.ID] = true;
                index++;
                return;
            }

            // Update IsBall property based on node traverse.
            if (dictionary != null)
            {
                if (dictionary.Any(d => d.Key == root.Left.ID && d.Value == true)) root.Left.IsBall = true;

                if (dictionary.Any(d => d.Key == root.Right.ID && d.Value == true)) root.Right.IsBall = true;
            }

            // If child node already have balls then make root node IsBall true. 
            if (root.Left.IsBall && root.Right.IsBall)
            {
                root.IsBall = true;
                return;
            }

            // Randomly take open gate in node based on IsBall property
            int gateOpen = -1;
            if (!root.Left.IsBall && !root.Right.IsBall)
            {
                gateOpen = (new Random()).Next(0, 2);
                if (gateOpen == 0) gateOpen = root.Left.ID;
                else gateOpen = root.Right.ID;
            }
            else gateOpen = (root.Left.IsBall == true ? root.Right.ID : root.Left.ID);

            // Traverse left / right node based on gateOpen
            if (gateOpen == root.Left.ID) traverse(root.Left, ref dictionary, ref index);
            else traverse(root.Right, ref dictionary, ref index);

        }

        public void print(Node root, String prefix)
        {
            if (root == null)
            {
                Console.WriteLine();
                return;
            }

            Console.WriteLine(prefix + "+- " + root.ID);
            print(root.Left, prefix + "|  ");
            print(root.Right, prefix + "|  ");
        }
    }
}
