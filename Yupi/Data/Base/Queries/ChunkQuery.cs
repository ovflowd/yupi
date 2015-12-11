/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System.Collections.Generic;
using System.Text;
using Yupi.Core.Util.Enumerators;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Data.Base.Queries
{
    /// <summary>
    /// Class DatabaseQueryChunk.
    /// </summary>
    internal class DatabaseQueryChunk
    {
        /// <summary>
        /// The _ending type
        /// </summary>
        private readonly TextEndingTypes _endingType;

        /// <summary>
        /// The _parameters
        /// </summary>
        private Dictionary<string, object> _parameters;

        /// <summary>
        /// The _queries
        /// </summary>
        private StringBuilder _queries;

        /// <summary>
        /// The _query count
        /// </summary>
        private int _queryCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseQueryChunk"/> class.
        /// </summary>
        public DatabaseQueryChunk()
        {
            _parameters = new Dictionary<string, object>();
            _queries = new StringBuilder();
            _queryCount = 0;
            _endingType = TextEndingTypes.Sequential;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseQueryChunk"/> class.
        /// </summary>
        /// <param name="startQuery">The start query.</param>
        public DatabaseQueryChunk(string startQuery)
        {
            _parameters = new Dictionary<string, object>();
            _queries = new StringBuilder(startQuery);
            _endingType = TextEndingTypes.Continuous;
            _queryCount = 0;
        }

        /// <summary>
        /// Adds the query.
        /// </summary>
        /// <param name="query">The query.</param>
        internal void AddQuery(string query)
        {
            _queryCount++;
            _queries.Append(query);

            switch (_endingType)
            {
                case TextEndingTypes.Sequential:
                    _queries.Append(';');
                    return;
                case TextEndingTypes.Continuous:
                    _queries.Append(',');
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        internal void AddParameter(string parameterName, object value)
        {
            _parameters.Add(parameterName, value);
        }

        /// <summary>
        /// Executes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Execute(IQueryAdapter dbClient)
        {
            if (_queryCount == 0)
                return;

            _queries = _queries.Remove(_queries.Length - 1, 1);

            dbClient.SetQuery(_queries.ToString());

            foreach (var current in _parameters)
                dbClient.AddParameter(current.Key, current.Value);

            dbClient.RunQuery();
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        internal void Dispose()
        {
            _parameters.Clear();
            _queries.Clear();
            _parameters = null;
            _queries = null;
        }
    }
}