using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LogLibrary
{
    public abstract class BaseContentWriter : IContentWriter
    {
        private ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private object _lock = new object();

        public BaseContentWriter()
        {
        }

        // Write to media
        public abstract bool WriteToMedia(string logcontent);

        async Task Flush()
        {
            string content;
            int count = 0;

            while (queue.TryDequeue(out content) && count <= 10)
            {
                // Write to Appropriate Media
                // Calls the Overriden method
                WriteToMedia(content);

                count++;
            }
        }

        public async Task<bool> Write(string content)
        {
            queue.Enqueue(content);

            if (queue.Count <= 10)
            {
                return true;
            }

            lock (_lock)
            {
                Task temp = Task.Run(() => Flush());
                Task.WaitAll(new Task[] { temp });
            }

            return true;
        }
    }
}
