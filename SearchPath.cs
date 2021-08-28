using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolaParcial
{
    class Node
    {
        public bool isExplored = false; // variable que indica si ya está explorada 
        public Node isExploredFrom = null; // variable que indica cual fue el nodo anterior
        public int X { get; set; }
        public int Y { get; set; }

        public Node(int _x, int _y)
        { //Costructor
            X = _x;
            Y = _y;
        }
    }
    class SearchPath
    {
        private static Dictionary<(int,int), Node> _block = new Dictionary<(int,int), Node>();                           // For storing all the nodes with Node.cs
        private static (int,int)[] _directions = {(0,1), (1,0), (0,-1), (-1,0)};    // Directions to search in BFS
        private static Queue<Node> _queue = new Queue<Node>();         // Queue for enqueueing nodes and traversing through them
        private static Node _searchingPoint;                           // Current node we are searching
        private static bool _isExploring = true;                       // If we are end then it is set to false
        private static Node startingPoint = null;
        private static Node endingPoint = null;

        private static List<Node> _path = new List<Node>();            // For storing the path traversed

        internal static Node StartingPoint { get => startingPoint; set => startingPoint = value; }
        internal static Node EndingPoint { get => endingPoint; set => endingPoint = value; }

        public static List<Node> Path(List<Node> noditos) // retorna el path
        {

                if (_path.Count == 0)                           // If we've already found path, no need to check it again
                {
                    LoadAllBlocks(noditos);
                    BFS();
                    CreatePath();
                }
                return _path;
         
        }

        // For getting all nodes with Node.cs and storing them in the dictionary
        private static void LoadAllBlocks(List<Node> nodos)
        {
            List<Node> laberinto = nodos; // Llenarlo con el nuevo laberinto creado en program.

            foreach (Node node in laberinto)
            {
                (int,int) gridPos = (node.X, node.Y);

                // For checking if 2 nodes are in same position; i.e overlapping nodes
                if (_block.ContainsKey(gridPos))
                {
                    Console.WriteLine("error, dos nodos iguales");
                }
                else
                {
                   
                    _block.Add(gridPos, node);        // Add the position of each node as key and the Node as the value
                }
            }

        }


        // BFS; For finding the shortest path
        private static void BFS()
        {
            _queue.Enqueue(StartingPoint);
            while (_queue.Count > 0 && _isExploring)
            {
                _searchingPoint = _queue.Dequeue();
                OnReachingEnd();
                ExploreNeighbourNodes();
            }
        }


        // To check if we've reached the Ending point
        private static void OnReachingEnd()
        {
            if (_searchingPoint == EndingPoint)
            {
                _isExploring = false;
             
            }
            else
            {
                _isExploring = true;
            }
        }


        // Searching the neighbouring nodes
        private static void ExploreNeighbourNodes()
        {
            if (!_isExploring) { return; }

            foreach ((int, int) direction in _directions)
            {

                (int, int) neighbourPos = (_searchingPoint.X + direction.Item1, _searchingPoint.Y + direction.Item2);
       
                if (_block.ContainsKey(neighbourPos))               // If the explore neighbour is present in the dictionary _block, which contians all the blocks with Node.cs attached
                {
                    //Console.WriteLine("Entré");
                    Node node = _block[neighbourPos];

                    if (!node.isExplored)
                    {
                        _queue.Enqueue(node);                       // Enqueueing the node at this position
                        node.isExplored = true;
                        node.isExploredFrom = _searchingPoint;      // Set how we reached the neighbouring node i.e the previous node; for getting the path
                    }
                }
            }
        }

        // Creating path using the isExploredFrom var of each node to get the previous node from where we got to this node
        public static void CreatePath()
        {
            SetPath(EndingPoint);
            Node previousNode = EndingPoint.isExploredFrom;

            while (previousNode != StartingPoint)
            {
                SetPath(previousNode);
                previousNode = previousNode.isExploredFrom;
            }

            SetPath(StartingPoint);
            _path.Reverse();
           
        
        }
        public static void PrintPath(List<Node> path) {

            foreach (var item in path)
            {
                Console.WriteLine(item.X.ToString() + "," + item.Y.ToString());
            }

        }
        // For adding nodes to the path
        private static void SetPath(Node node)
        {
            _path.Add(node);
        }
               
    }
}
