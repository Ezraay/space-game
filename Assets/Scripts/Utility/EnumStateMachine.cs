using System;
using System.Collections.Generic;

namespace Spaceships.Utility
{
    public class EnumStateMachine<T> : StateMachine where T : Enum
    {
        private readonly Dictionary<T, List<EnumTransition>> transitions = new Dictionary<T, List<EnumTransition>>();

        public EnumStateMachine(T startingState) : base((int) (object) startingState)
        {
            State = startingState;
        }

        public new T State { get; private set; }

        public void AddTransition(T from, int to, TransitionFunction function, OnTransition onTransition)
        {
            base.AddTransition((int) (object) from, to, function, onTransition);
        }

        public override void CheckTransitions()
        {
            List<EnumTransition> currentStateTransitions = transitions[State];
            foreach (EnumTransition transition in currentStateTransitions)
            {
                if (transition.Valid())
                {
                    State = transition.to;
                    transition.onTransition();
                    break;
                }
            }
        }

        public class EnumTransition : Transition
        {
            public new readonly T to;

            public EnumTransition(T to, TransitionFunction function, OnTransition onTransition) : base(
                (int) (object) to, function, onTransition)
            {
                this.to = to;
            }
        }
    }
}