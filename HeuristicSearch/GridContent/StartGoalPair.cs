using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HeuristicSearch.GridContent
{
    [Serializable]
    public class StartGoalPair
    {
        [NonSerialized]
        private Vector2 start;

        [NonSerialized]
        private Vector2 goal;

        public Vector2 Start { get { return start; } set { start = value; } }
        public Vector2 Goal { get { return goal; } set { goal = value; } }

        public override string ToString()
        {
            string startLabel = string.Format("Start: ({0}, {1})", Start.X, Start.Y);
            string goalLabel = string.Format("Goal: ({0}, {1})", Goal.X, Goal.Y);

            return string.Format("{0, -25}{1, -25}", startLabel, goalLabel);
        }

        public override bool Equals(object obj)
        {
            return obj is StartGoalPair 
                && (obj as StartGoalPair).Start.Equals(this.Start) 
                && (obj as StartGoalPair).Goal.Equals(this.Goal);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
