using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree binarySearchTree = new BinarySearchTree();

            binarySearchTree.insert(5);
            binarySearchTree.insertReturn(6);
            binarySearchTree.insert(-1);
            binarySearchTree.insertReturn(7);
            binarySearchTree.insert(-2);
            binarySearchTree.insert(3);
            binarySearchTree.insert(4);
            binarySearchTree.insert(1);
            binarySearchTree.insert(2);
            binarySearchTree.insert(0);


            Console.WriteLine("Cay sau khi chen");
            Console.WriteLine(binarySearchTree.ToString());

            Console.WriteLine("Tim phan tu");
            int pt = 0;
            Node ktra = binarySearchTree.search(pt);

            if (ktra != null)
                Console.WriteLine("pt " + pt + " co ton tai trong cay");
            else
                Console.WriteLine("pt " + pt + " khong ton tai trong cay");

            Console.Write("Do sau cua cay: " + binarySearchTree.depth() + '\n');

            Console.WriteLine("Chuyen cay sang mang");
            List<int> a = binarySearchTree.chuyenMang();

            foreach (int gt in a)
                Console.Write(gt + " ");

            Console.WriteLine();
            binarySearchTree.remove(5);
            Console.WriteLine("Cay sau khi xoa");
            Console.WriteLine(binarySearchTree.ToString());
        }
    }
}

class Node
{
    public Node left = null;
    public Node right = null;
    public int key;

    public Node(int key)
    {
        this.key = key;
    }

    public override string ToString()
    {
        return key + " ";
    }
}

class BinarySearchTree
{
    private Node root = null;

    public void insert(int value)
    {
        if (root == null)
        {
            root = new Node(value);
            return;
        }

        doInsert(root, value);
    }

    private void doInsert(Node node, int value)
    {
        Node q = node;

        while (q != null)
        {
            Node nodeTam = q;

            if (q.key == value)
                return;

            if (q.key < value)
            {
                q = q.right;

                if (q == null)
                    nodeTam.right = new Node(value);
            }
            else
            {
                q = q.left;

                if (q == null)
                    nodeTam.left = new Node(value);
            }
        }
    }

    public void insertReturn(int key)
    {
        root = doinsertReturn(root, key);
    }

    Node doinsertReturn(Node node, int value)
    {
        if (node == null)
        {
            node = new Node(value);
            return node;
        }

        if (value < node.key)
            node.left = doinsertReturn(node.left, value);
        else if (value > node.key)
            node.right = doinsertReturn(node.right, value);

        return node;
    }

    private void doChuyenMang(Node node, List<int> a)
    {
        if (node == null)
            return;

        doChuyenMang(node.left, a);
        a.Add(node.key);
        doChuyenMang(node.right, a);
    }

    public List<int> chuyenMang()
    {
        List<int> a = new List<int>();

        doChuyenMang(root, a);

        return a;
    }

    public Node search(int val)
    {
        return doSearch(root, val);
    }

    private Node preNode;

    private Node doSearch(Node node, int value)
    {
        if (node == null)
            return null;

        if (node.key == value)
            return node;

        if (node.key > value)
        {
            preNode = node;
            return doSearch(node.left, value);
        }
        else
        {
            preNode = node;
            return doSearch(node.right, value);
        }
            
    }

    private int tinhDoSau(Node node)
    {
        if (node == null)
            return -1;

        return Math.Max(tinhDoSau(node.left), tinhDoSau(node.right)) + 1;
    }

    public int depth()
    {
        return tinhDoSau(root);
    }

    public override string ToString()
    {
        return ToString(root);
    }

    public string ToString(Node node)
    {
        string s = "";

        if (node == null)
            return "";

        s += ToString(node.left);
        s += node.key + " ";
        s += ToString(node.right);

        return s;
    }

    Node minValueLeft(Node node)
    {
        while (node.left != null)
        {
            preNode = node;
            node = node.left;
        }

        return node;
    }

    private void doRemove(Node node, int key)
    {
        Node p = search(key);

        if (p == null)
            return;

        if (p.left == null && p.right == null)
        {
            if (p == root)
            {
                root = null;
                return;
            }

            if (preNode.left == p)
                preNode.left = null;
            else
                preNode.right = null;
        }
        else if (p.left == null)
        {
            if (p == root)
            {
                root = root.right;
                return;
            }

            preNode.right = p.right;
        }
        else if (p.right == null)
        {
            if (p == root)
            {
                root = root.left;
                return;
            }

            preNode.left = p.left;
        }
        else
        {
            if (p.right.left == null)
            {
                p.key = p.right.key;
                p.right = p.right.right;
            }
            else
            {
                Node succ = p.right;
                Node succPre = p;

                succ = minValueLeft(succ);

                p.key = succ.key;
                preNode.left = succ.right;
            }
        }
    }

    public void remove(int key)
    {
        doRemove(root, key);
    }
}
