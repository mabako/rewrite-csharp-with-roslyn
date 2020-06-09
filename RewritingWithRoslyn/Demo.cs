using System;
using System.Threading;

namespace RewritingWithRoslyn
{
    public class Demo
    {
        public void DoStuff()
        {
            // TODO(first): Reduce to 50 once the rest of the logic is there
            Thread.Sleep(1000);

            /* TODO(second): This implementation is suboptimal.
             * I've looked into it in the past, but haven't had the time
             * to properly test a new solution. */
            throw new NotImplementedException();
        }
    }
}
