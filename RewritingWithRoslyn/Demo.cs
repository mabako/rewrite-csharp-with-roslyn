using System;
using System.Threading;

namespace RewritingWithRoslyn
{
    public class Demo
    {
        public void DoSyntacticStuff()
        {
            // TODO(first): Reduce to 50 once the rest of the logic is there
            Thread.Sleep(1000);

            /* TODO(second): This implementation is suboptimal.
             * I've looked into it in the past, but haven't had the time
             * to properly test a new solution. */
            throw new NotImplementedException();
        }

        public void DoSemanticStuff()
        {
            var obj = new B();
            string name = "X";
            Console.WriteLine(obj.SayHello(name));
        }

        class A
        {
            public virtual string SayHello(string name) => $"Hello, {name}";
            public string SayHello(int x) => $"Who, {x}?";
        }

        class B : A
        {
            public override string SayHello(string name) => $"Good Morning, {name}";
        }
    }
}
