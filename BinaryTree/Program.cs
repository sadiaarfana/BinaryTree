using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize variables
            int j = 2;
            int level, ball, SIZE = 2;

            // Input Tree level
            Console.Write("Level: ");
            if (int.TryParse(Console.ReadLine(), out level))
            {
                // Input no of balls
                Console.Write("Ball: ");
                if (int.TryParse(Console.ReadLine(), out ball))
                {
                    // Calculate total nodes based on level
                    Tree bst = new Tree(1);
                    for (int i = 2; i <= level; i++)
                    {
                        j = j * 2;
                        SIZE = SIZE + j;
                    }
                    SIZE += 1;

                    // Build tree
                    Tree builder = new Tree(SIZE);
                    Node tree = builder.root;

                    // Print tree
                    //Console.WriteLine("Print Tree: ");
                    //bst.print(tree, "");
                    //Console.WriteLine();

                    // Initialize array for containers in last level with value false. 
                    // False means that container doesn't have any ball and true is vice versa                    
                    var dictionary = new Dictionary<int, bool>();
                    int minNode = SIZE - j + 1, maxNode= SIZE;

                    for (int i = minNode; i <= maxNode; i++)
                    {
                        dictionary.Add(i, false);
                    }

                    // Prediction
                    Console.WriteLine("Prediction: Below Containers  don't receive any ball");
                    var r = new Random();
                    var predictionList = new List<int>();
                    var dictionaryList = dictionary.ToList();
                    for (int i = 0; i < j - ball;)
                    {
                        var n = dictionaryList[r.Next(1, j)].Key;
                        
                        if (!predictionList.Contains(n))
                        {
                            predictionList.Add(n);
                            Console.Write("{0}  ", predictionList[i++]-j+1);                            
                        }
                    }
                    Console.WriteLine();

                    // Tree traverse for each ball
                    for (int i = 0; i < ball;)
                    {
                        bst.traverse(tree, ref dictionary, ref i);
                    }

                    // Take list of containers which don't have any ball.
                    var nodes = dictionary.Where(a => a.Value == false).ToList();

                    // Result Output
                    // container no starts with 1 
                    Console.WriteLine("Result: Below Containers don't receive any ball");
                    foreach (var node in nodes)
                    {
                        Console.Write("{0}  ", node.Key-j+1);
                    }
                    Console.WriteLine();

                    //Compare Prediction and Results in %
                    var rightAnswers = nodes.Where(n => predictionList.Contains(n.Key)).ToList();
                    double matchPercentage = ((double)rightAnswers.Count / (double)predictionList.Count) * 100;
                    Console.WriteLine("Match: {0:F2}%", matchPercentage);
                }
                else Console.WriteLine("No valid input provided.");
            }
            else Console.WriteLine("No valid input provided.");

            Console.ReadKey();


        }
    }
}
