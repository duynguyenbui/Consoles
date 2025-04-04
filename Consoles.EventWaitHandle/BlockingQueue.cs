using System.Threading.Tasks;
namespace Consoles.EventWaitHandle;


public class BlockingQueue<T>
{
    private readonly List<T> _queue = [];
    private readonly System.Threading.EventWaitHandle _eventWaitHandle = new(false, EventResetMode.AutoReset);

    public void EnQueue(T item)
    {
        _queue.Add(item);
        
        _eventWaitHandle.Set();
    }

    public T DeQueue()
    {
        _eventWaitHandle.WaitOne();
        
        var item = _queue[0];
        _queue.RemoveAt(0);

        return item;
    }
}