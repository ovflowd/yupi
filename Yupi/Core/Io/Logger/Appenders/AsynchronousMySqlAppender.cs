using System;
using System.Collections.Generic;
using System.Threading;
using log4net.Appender;
using log4net.Core;
using MySql.Data.MySqlClient;

namespace Yupi.Core.Io.Logger.Appenders
{
    public class AsynchronousMySqlAppender : AdoNetAppender
    {
        private readonly Queue<LoggingEvent> _pendingTasks;

        private readonly object _lockObject = new object();

        private readonly ManualResetEvent _manualResetEvent;

        private bool _onClosing;

        public AsynchronousMySqlAppender()
        {
            MySqlConnectionStringBuilder stringBuilder = Yupi.GetDatabaseManager().GetConnectionStringBuilder();

            ConnectionType = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";
            CommandText = "INSERT INTO server_system_logs (date,thread,level,logger,message,exception) VALUES (?log_date, ?thread, ?log_level, ?logger, ?message, ?exception)";
            ConnectionString = $"Server={stringBuilder.Server};Database={stringBuilder.Database};Uid={stringBuilder.UserID};Pwd={stringBuilder.Password};Port={stringBuilder.Port};";

            _pendingTasks = new Queue<LoggingEvent>();

            _manualResetEvent = new ManualResetEvent(false);

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
            if (!_onClosing)
            {
                Thread thread = new Thread(LogMessages);

                thread.Start();
            }
        }

        private void LogMessages()
        {
            while (!_onClosing)
            {
                LoggingEvent loggingEvent;

                while (!DeQueue(out loggingEvent))
                {
                    Thread.Sleep(10);

                    if (_onClosing)
                        break;
                }

                if (loggingEvent != null)
                    base.Append(loggingEvent);
            }

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

        protected override void OnClose()
        {
            _onClosing = true;

            _manualResetEvent.WaitOne(TimeSpan.FromSeconds(10));

            base.OnClose();
        }
    }
}