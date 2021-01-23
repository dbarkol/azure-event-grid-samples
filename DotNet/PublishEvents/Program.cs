/*********************************************************************************************
    This sample demonstrates how to publish events to Azure Event Grid
    by using the Azure .NET SDK. 

    Requirements:
        * An Event Grid custom topic to send events to.
        * The Event Grid topic endpoint and access key for making the requests.        

    References:
        * Event Grid Overview: https://docs.microsoft.com/en-us/azure/event-grid/overview
        * NuGet Package: https://www.nuget.org/packages/Microsoft.Azure.EventGrid/
        * Azure SDK for .NET: https://github.com/Azure/azure-sdk-for-net        
*********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Rest;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using PublishEvents.Data;

namespace PublishEvents
{
    internal class Program
    {
        #region Data Members

        // The custom topic host name
        // Example: {topicname}.westus2-1.eventgrid.azure.net
        private const string TopicHostName = "{topic-host-name}";        

        // Custom topic access key
        private const string TopicKey = "{custom-topic-access-key}";

        #endregion

        #region Private Methods

        private static async Task Main(string[] args)
        {
            await PublishEvents();
        }

        private static async Task PublishEvents()
        {
            // Create service credential with the topic credential
            // class and custom topic access key
            ServiceClientCredentials credentials = new TopicCredentials(TopicKey);

            // Create an instance of the event grid client class
            var client = new EventGridClient(credentials);

            // Retrieve a collection of events
            var events = GetEvents();

            // Publish the events
            await client.PublishEventsAsync(
                TopicHostName, 
                events);
        }

        private static IList<EventGridEvent> GetEvents()
        {
            // Return a list of events 
            var events = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    Data = new Musician()
                    {
                        Name = "Eddie Van Halen",
                        Instruments = "Guitar, Keyboards"
                    },
                    EventTime = DateTime.Now,
                    EventType = "EventGrid.Sample.MusicianAdded",
                    Subject = "Musicians",
                    DataVersion = "1.0"
                },
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    Data = new Musician()
                    {
                        Name = "Slash",
                        Instruments = "Guitar"
                    },
                    EventTime = DateTime.Now,
                    EventType = "EventGrid.Sample.MusicianAdded",
                    Subject = "Musicians",
                    DataVersion = "1.0"
                }
            };

            return events;            
        }

        #endregion

        }

}
