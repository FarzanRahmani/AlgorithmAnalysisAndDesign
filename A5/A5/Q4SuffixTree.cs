using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q4SuffixTree : Processor
    {
        public Q4SuffixTree(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String[]>)Solve);

        public string[] Solve(string text)
        {
            SuffixTree sufTree = new SuffixTree(text);
            text = null;
            return sufTree.Edges();
        }

        public class Node
        {
            public string label;
            public Dictionary<char, Node> outgoingEdges;

            public Node(string label)
            {
                this.label = label;
                this.outgoingEdges = new Dictionary<char, Node>();
            }
        }

        public class SuffixTree
        {
            public Node root;
            List<string> ans;

            public SuffixTree(string text)
            {
                ans = new List<string>(2*text.Length);
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
                            while (k - j < label.Length && text[k] == label[k - j])
                                k += 1;

                            if (k - j == label.Length)
                            {
                                currentNode = childNode;
                                j = k;
                            }
                            else
                            {
                                char charExist = label[k - j];
                                char charNew = text[k];
                                Node middleNode = new Node(label.Substring(0, k - j)); //
                                middleNode.outgoingEdges[charNew] = new Node(text.Substring(k));
                                childNode.label = label.Substring(k - j);
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

            public string[] Edges()
            {
                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(this.root);
                while (queue.Count > 0)
                {
                    Node tmp = queue.Dequeue();
                    if (tmp != this.root)
                        ans.Add(tmp.label);
                    foreach (Node neighbor in tmp.outgoingEdges.Values)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
                return ans.ToArray();
            }
        }

    }
}
