using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Hjelpeklasser
{
    public class DigitalTwinsFunctions : IDigitalTwinsBuilder
    {
        public Task CreateRelationshipAsync(DigitalTwinsClient client, string digitalTwinSrcId, string digitalTwinTargetId)
        {
            throw new NotImplementedException();
        }

        public BasicDigitalTwin CreateTwinContents(string modelId)
        {
            throw new NotImplementedException();
        }

        public void CreateTwins(string id, BasicDigitalTwin basicDigitalTwin)
        {
            throw new NotImplementedException();
        }
    }
}
