using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.RabbitMQ
{
    public interface IEmailQueuePublisher : IDisposable
    {
        void Publish(string message);
    }

}
