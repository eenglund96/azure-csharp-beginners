using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.API.Client
{
    /// <summary>
    /// Create this class with Edit->Paste Special->Paste JSON As Classes
    /// When writing a client for a REST API we typically don't have access to the source code of the service.
    /// To make it easier to interact with the service we need to create our own local representation of the contract (request and response messages)
    /// A common way to construct these classes is to use `Paste JSON as Classes` in Visual Studio
    /// Paste JSON as classes will try to infer the type of each property from values in the JSON in a best effort manner. 
    /// Be sure to always check that the types are what we want them to be.
    /// </summary>
    public class Greeting                       //Always rename the default name "Rootobject" to the correct class name
    {
        /// <summary>
        /// Paste JSON as classes will think this is a string and that's fine for most cases. But for our application, change this to a GUID
        /// </summary>
        public Guid id { get; set; } = Guid.NewGuid();      //generate guid as default
        public string message { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public DateTime timestamp { get; set; }
    }

}
