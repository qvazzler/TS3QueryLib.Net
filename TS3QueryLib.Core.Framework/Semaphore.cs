using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TS3QueryLib.Core
{
    public class Semaphore
    {
        private object Mutex { get; set; }
        private int Count { get; set; }
        private int Max { get; set; }

        public Semaphore(int max = 1)
        {
            Mutex = new object();
            Max = max;
        }

        public void WaitOne()
        {
            while (true)
            {
                lock (Mutex)
                {
                    if (Count < Max)
                    {
                        Count++;
                        return;
                    }
                }

                Thread.Sleep(50);
            }
        }

        public void Release()
        {
            lock (Mutex)
            {
                if (Count >= 0)
                {
                    Count--;
                }
            }
        }
    }
}
