using System;
using System.Collections.Generic;
using System.Text;

namespace Aiv.Fast2D.Component
{
    public interface IAStarAction<T>
    {
        /// <summary>
        /// State where we go after doing the action
        /// </summary>
        T NextState { get; }
        /// <summary>
        /// Cost of performing the action
        /// </summary>
        float Cost { get; }
    }

    public interface IAStarState<T> where T : IAStarState<T>
    {
        /// <summary>
        /// Check if _other is equal to this
        /// </summary>
        /// <param name="_other">The node to check for equality</param>
        /// <returns>true if _other is equal to this, false otherwise</returns>
        bool Equals(T _other);
        /// <summary>
        /// Estimate cost from this to _other
        /// </summary>
        /// <param name="_other">The node to which the estimation is computed</param>
        /// <returns>The value of the estimated cost to reach _other</returns>
        float Heuristic(T _other);
        /// <summary>
        /// List of links
        /// </summary>
        List<IAStarAction<T>> Actions { get; }
    }

    public class AStarNode<T> where T : IAStarState<T>
    {
        /// <summary>
        /// The node with the shortest path to this
        /// </summary>
        public AStarNode<T> Parent;
        /// <summary>
        /// The node element
        /// </summary>
        public T State { get; }
        /// <summary>
        /// All nodes reachable from this
        /// </summary>
        public List<AStarNode<T>> Neighbours { get { return GetNeighbours(); } }
        /// <summary>
        /// Cost of reaching this from start
        /// </summary>
        public float PathCost;
        /// <summary>
        /// PathCost + Heuristic to objective
        /// </summary>
        public float TotalCost;
        /// <summary>
        /// Check if _other is equal to this
        /// </summary>
        /// <param name="_other">The node to check for equality</param>
        /// <returns>true if _other is equal to this, false otherwise</returns>
        public bool Equals(AStarNode<T> _other)
        {
            return State.Equals(_other.State);
        }
        /// <summary>
        /// Test if this is the objective
        /// </summary>
        /// <param name="_objective">The objective</param>
        /// <returns>true if this is the objective, fasle otherwise</returns>
        public bool Test(T _objective)
        {
            return State.Equals(_objective);
        }
        /// <summary>
        /// Estimate cost from this to _other
        /// </summary>
        /// <param name="_other">The node to which the estimation is computed</param>
        /// <returns>The value of the estimated cost to reach _other</returns>
        public float Heuristic(T _other)
        {
            return State.Heuristic(_other);
        }
        /// <summary>
        /// Construct an AStarNode
        /// </summary>
        /// <param name="_element">State of this node</param>
        /// <param name="_parent">Node originating this</param>
        /// <param name="_cost">Cost of reaching this from parent</param>
        public AStarNode(T _element, AStarNode<T> _parent, float _cost)
        {
            State = _element;
            Parent = _parent;
            PathCost = _cost;
            if (_parent != null)
            {
                PathCost += _parent.PathCost;
            }
        }
        /// <summary>
        /// Get list of reachable nodes given the available actions
        /// </summary>
        /// <returns>List of nodes</returns>
        private List<AStarNode<T>> GetNeighbours()
        {
            List<AStarNode<T>> result = new List<AStarNode<T>>();
            foreach (var action in State.Actions)
            {
                result.Add(new AStarNode<T>(action.NextState, this, action.Cost));
            }
            return result;
        }
    }

    public class AStar<T> where T : class, IAStarState<T>
    {
        private List<AStarNode<T>> m_OpenList;
        private List<AStarNode<T>> m_ClosedList;

        public List<AStarNode<T>> Border { get { return m_OpenList; } }
        public T Objective { get; private set; }
        public AStarNode<T> Current { get { return m_OpenList.Count > 0 ? m_OpenList[0] : null; } }

        /// <summary>
        /// Helper method to run an A* search
        /// </summary>
        /// <param name="_start">starting point</param>
        /// <param name="_objective">destination</param>
        /// <returns>Destination node</returns>
        public static AStarNode<T> Find(T _start, T _objective)
        {
            AStar<T> pathfind = new AStar<T>(_start, _objective);
            while (!pathfind.Step())
            { }
            return pathfind.Current;
        }

        public AStar(T _start, T _objective)
        {
            m_OpenList = new List<AStarNode<T>>();
            m_ClosedList = new List<AStarNode<T>>();

            Objective = _objective;
            var root = new AStarNode<T>(_start, null, 0);
            root.TotalCost = root.Heuristic(Objective);
            m_OpenList.Add(root);
        }

        /// <summary>
        /// Advance A* algorithm by 1 step
        /// </summary>
        /// <returns>true if algorithm is over, false otherwise</returns>
        public bool Step()
        {
            if (m_OpenList.Count > 0)
            {
                var current = m_OpenList[0];
                if (current.Test(Objective))
                {
                    return true;
                }

                m_OpenList.RemoveAt(0);
                m_ClosedList.Add(current);

                foreach (var neighbours in current.Neighbours)
                {
                    InsertSorted(neighbours, Objective);
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Insert _node in the open list.
        /// If the node is in the closed list, the node is not added.
        /// If the node is already in the open list, the best of the two is kept.
        /// </summary>
        /// <param name="_node">node to add</param>
        /// <param name="_objective">objective to which the heuristic is to be checked</param>
        private void InsertSorted(AStarNode<T> _node, T _objective)
        {
            // If the node has already been evaluated, ignore it
            if (m_ClosedList.Find(node => node.Equals(_node)) == null)
            {
                // compute total cost of the node
                _node.TotalCost = _node.PathCost + _node.Heuristic(_objective);

                // check if we already have the node in open list
                var current = m_OpenList.Find(node => node.Equals(_node));
                if (current == null || current.TotalCost > _node.TotalCost)
                {
                    // if the node is not in the open list, or the node in the open list is worse, add the current node to the open list and remove the worse copy
                    m_OpenList.Insert(BinarySearch(_node), _node); // Since the open list is sorted from best to worst, we use binary search to find where to insert the new node
                    if (current != null)
                    {
                        m_OpenList.Remove(current);
                    }
                }
            }
        }

        private int BinarySearch(AStarNode<T> _node)
        {
            int begin = 0;
            int end = m_OpenList.Count;
            while (end - begin > 1)
            {
                int curr = (begin + end) / 2;
                if (m_OpenList[curr].TotalCost <= _node.TotalCost)
                {
                    begin = curr;
                }
                else
                {
                    end = curr;
                }
            }

            if (end > begin && m_OpenList[begin].TotalCost <= _node.TotalCost)
            {
                return end;
            }
            return begin;
        }
    }
}
