using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class MinMax
    {
        Node head;
        public Node CurrentNode;

        public MinMax()
        {
            CurrentNode = new Node();
            head = CurrentNode;
            createChildren(CurrentNode, TileState.Cpu);
        }
        public MinMax(int move)
        {
            CurrentNode = new Node(move);
            head = CurrentNode;
            createChildren(CurrentNode, TileState.Cpu);
        }

        public void PlayerMove(int move)
        {
            CurrentNode = CurrentNode.Children.FirstOrDefault(x => x.TileStates[move] == TileState.User);

            if (CurrentNode.Children == null)
            {
                return;
            }
        }

        public (int, GameStatus) BestMove()
        {
            if(CurrentNode.TileStates[4] == TileState.None)
            {
                CurrentNode = CurrentNode.Children.FirstOrDefault(x => x.TileStates[4] == TileState.Cpu);
            }
            else
            {
                List<int> mins = CurrentNode.Children.Select(x => x.Min).ToList();
                CurrentNode = CurrentNode.Children[mins.IndexOf(mins.Max())];
            }

            if(CurrentNode.Status != GameStatus.None)
            {
                CurrentNode.Move = -1;
            }

            return (CurrentNode.Move, CurrentNode.Status);
        }
        
        private void createChildren(Node head, TileState move)
        {
            List<Node> list = new List<Node>();
            list.Add(head);

            while(list.Count > 0)
            {
                list = createLevel(list, move);

                move = move == TileState.Cpu ? TileState.User : TileState.Cpu;
            }
        }
        private List<Node> createLevel(List<Node> nextLevel, TileState move)
        {
            Node[] currentLevel = nextLevel.ToArray();
            nextLevel.Clear();

            foreach (var node in currentLevel)
            {
                //do not make children for finished games
                if (node.Status != GameStatus.None)
                    continue;

                //foreach tile that is none
                //make a new child with a move of that index
                //children where the tile has been taken are null
                //then remove the null children

                node.Children = node.TileStates
                        .Select((tile, index) => tile != TileState.None ? null : new Node(node, index, move == TileState.Cpu ? TileState.Cpu : TileState.User))
                        .Where(child => child != null)
                        .ToArray();

                //store the children to
                foreach (var child in node.Children)
                {
                    nextLevel.Add(child);
                }
            }
            //create children for the next level
            return nextLevel;
        }


        /* Postorder traversal results in stack overflow
        private void CreateChildren(Node currentNode, TileState move)
        {
            if (currentNode.Status != GameStatus.None)
                return;

            currentNode.Children = currentNode.TileStates
                .Where(tile => tile == TileState.None)
                .Select((tile, index) => new Node(currentNode, index, move == TileState.Cpu ? TileState.Cpu : TileState.User))
                .ToArray();

            foreach (var child in currentNode.Children)
            {
                CreateChildren(child, move == TileState.Cpu ? TileState.Cpu : TileState.User);
            }
        }*/
    }
}
