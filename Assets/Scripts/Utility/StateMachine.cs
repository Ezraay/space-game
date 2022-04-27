using System.Collections.Generic;

namespace Spaceships.Utility
{
    public class StateMachine
    {
        public delegate void OnTransition();

        public delegate bool TransitionFunction();

        private readonly Dictionary<int, List<Transition>> transitions = new Dictionary<int, List<Transition>>();

        public StateMachine(int startingState)
        {
            State = startingState;
        }

        public int State { get; private set; }

        public virtual void AddTransition(int from, int to, TransitionFunction function, OnTransition onTransition)
        {
            if (!transitions.ContainsKey(from))
                transitions.Add(from, new List<Transition>());

            transitions[from].Add(new Transition(to, function, onTransition));
        }

        public virtual void CheckTransitions()
        {
            List<Transition> currentStateTransitions = transitions[State];
            foreach (Transition transition in currentStateTransitions)
            {
                if (transition.Valid())
                {
                    State = transition.to;
                    transition.onTransition();
                    break;
                }
            }
        }

        public class Transition
        {
            protected readonly TransitionFunction function;
            public readonly OnTransition onTransition;
            public readonly int to;

            public Transition(int to, TransitionFunction function, OnTransition onTransition)
            {
                this.to = to;
                this.function = function;
                this.onTransition = onTransition;
            }

            public bool Valid()
            {
                return function();
            }
        }
    }
}