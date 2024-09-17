using System;
using System.Collections.Generic;
using System.Linq;

public class Node
    {
        public string label;
        public Dictionary<char,Node> outgoingEdges;

        public Node(string label)
        {
            this.label = label;
            this.outgoingEdges = new Dictionary<char, Node>();
        }
    }
    
    public class SuffixTree
    {
        public Node root;

        public SuffixTree(string text)
        {
            this.root = new Node("");
            this.root.outgoingEdges[text[0]] = new Node(text);
            for (int i = 1; i < text.Length; i++)
            {
                Node currentNode = this.root;
                int j = i;
                while (j < text.Length)
                {
                    if (currentNode.outgoingEdges.ContainsKey(text[j]))
                    {
                        Node childNode = currentNode.outgoingEdges[text[j]];
                        string label = childNode.label;

                        int k = j + 1;
                        while (k-j < label.Length && text[k] == label[k-j])
                            k += 1;
                        
                        if (k-j == label.Length)
                        {
                            currentNode = childNode;
                            j = k;
                        }
                        else
                        {
                            char charExist = label[k - j];
                            char charNew = text[k];
                            Node middleNode = new Node(label.Substring(0,k-j)); //
                            middleNode.outgoingEdges[charNew] = new Node(text.Substring(k));
                            childNode.label = label.Substring(k-j);
                            middleNode.outgoingEdges[charExist] = childNode;
                            currentNode.outgoingEdges[text[j]] = middleNode;
                        }
                    }
                    else
                    {
                        currentNode.outgoingEdges[text[j]] = new Node(text.Substring(j)); 
                    }
                }
            }
        }

        public void WriteEdges()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(this.root);
            while (queue.Count > 0)
            {
                Node tmp = queue.Dequeue();
                if (tmp != this.root)
                    Console.WriteLine(tmp.label);
                foreach (Node neighbor in tmp.outgoingEdges.Values)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
    public class Program
    {

        static public void Main(String[] args)
        {
            string text = Console.ReadLine();
            SuffixTree sufTree = new SuffixTree(text);
            text = null;
            sufTree.WriteEdges();
        }
    }