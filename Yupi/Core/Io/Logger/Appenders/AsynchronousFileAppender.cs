using System;
using System.Collections.Generic;
using System.Threading;
using log4net.Appender;
using log4net.Core;

namespace Yupi.Core.Io.Logger.Appenders
{
    public class AsynchronousFileAppender : RollingFileAppender
    {
        private readonly Queue<LoggingEvent> _pendingTasks;
        private readonly object _lockObject = new object();
        private readonly ManualResetEvent _manualResetEvent;
        private bool _onClosing;

        public AsynchronousFileAppender()
        {
            //initialize our queue
            _pendingTasks = new Queue<LoggingEvent>();

            //put the event initially in non-signalled state
            _manualResetEvent = new ManualResetEvent(false);

            //start the asyn process of handling pending tasks
            Start();
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            foreach (LoggingEvent loggingEvent in loggingEvents)
                Append(loggingEvent);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (FilterEvent(loggingEvent))
                Enqueue(loggingEvent);
        }

        private void Start()
        {
            //hopefully user doesnt open and close the GUI or CONSOLE OR WEBPAGE
            //right away. anyway lets add that condition too
            if (!_onClosing)
            {
                Thread thread = new Thread(LogMessages);
                thread.Start();
            }
        }

        private void LogMessages()
        {
            //we keep on processing tasks until shutdown on repository is called
            while (!_onClosing)
            {
                //fetch the next item from the pending queue
                LoggingEvent loggingEvent;

                while (!DeQueue(out loggingEvent))
                {
                    //if they are no pending tasks sleep 10 seconds and try again
                    Thread.Sleep(10);

                    //if closing is already initiated break
                    if (_onClosing)
                        break;
                }

                //write the last event fetched from the queue to the log
                if (loggingEvent != null)
                {
                    base.Append(loggingEvent);
                }
            }

            //we are done with our logging, sent the signal to the parent thread
            //so that it can commence shut down
            _manualResetEvent.Set();
        }

        /// <summary>
        /// add the event to our pending queue
        /// </summary>       
        private void Enqueue(LoggingEvent loggingEvent)
        {
            lock (_lockObject)
                _pendingTasks.Enqueue(loggingEvent);
        }

        /// <summary>
        /// fetch the object at the beginning of the queue
        /// </summary>       
        private bool DeQueue(out LoggingEvent loggingEvent)
        {
            lock (_lockObject)
            {
                if (_pendingTasks.Count > 0)
                {
                    loggingEvent = _pendingTasks.Dequeue();
                    return true;
                }

                loggingEvent = null;
                return false;
            }
        }

        //OnClose method is called, when the shut down of the repository is
        //invoked
        protected override void OnClose()
        {
            //set the OnClosing flag to true, so that
            //AppendLoggingEvents would know it is time to wrap up
            //whatever it is doing
            _onClosing = true;

            //wait till we receive signal from manualResetEvent
            //which is signalled from AppendLoggingEvents
            _manualResetEvent.WaitOne(TimeSpan.FromSeconds(10));

            //manualResetEvent.WaitOne();
            base.OnClose();
        }
    }

}
