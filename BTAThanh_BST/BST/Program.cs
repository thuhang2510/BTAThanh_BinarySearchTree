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
            binarySearchTree.insert(3);
            binarySearchTree.insertReturn(8);
            binarySearchTree.insert(4);
            binarySearchTree.insert(7);

            Console.WriteLine("Cay sau khi chen");
            Console.WriteLine(binarySearchTree.ToString());

            Console.WriteLine("Tim phan tu");
            int pt = 0;
            bool ktra = binarySearchTree.search(pt);

            if (ktra == true)
                Console.WriteLine("pt " + pt + " co ton tai trong cay");
            else
                Console.WriteLine("pt " + pt + " khong ton tai trong cay");

            Console.Write("Do sau cua cay: " + binarySearchTree.depth() + '\n');

            Console.WriteLine("Chuyen cay sang mang");
            List<int> a = binarySearchTree.chuyenMang();

            foreach (int gt in a)
                Console.Write(gt + " ");

            Console.WriteLine();
            binarySearchTree.remove(-1);
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
        Node newNode = new Node(value);

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
                    nodeTam.right = newNode;
            }
            else
            {
                q = q.left;

                if (q == null)
                    nodeTam.left = newNode;
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

    public bool search(int val)
    {
        return doSearch(root, val);
    }

    private bool doSearch(Node node, int value)
    {
        if (node == null)
            return false;

        if (node.key == value)
            return true;

        if (node.key > value)
            return doSearch(node.left, value);
        else
            return doSearch(node.right, value);
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

    int minValueRight(Node node)
    {
        int minv = node.key;
        while (node.left != null)
        {
            minv = node.left.key;
            node = node.left;
        }
        return minv;
    }

    Node doRemove(Node node, int key)
    {
        if (node == null)
            return node;

        if (key < node.key)
            node.left = doRemove(node.left, key);
        else if (key > node.key)
            node.right = doRemove(node.right, key);
        else
        {
            if (node.left == null)
                return node.right;
            else if (node.right == null)
                return node.left;

            node.key = minValueRight(node.right);
            node.right = doRemove(node.right, node.key);
        }

        return node;
    }

    public void remove(int key)
    {
        root = doRemove(root, key);
    }
}
