using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Interface
{
    public interface IDigitalTwinsBuilder
    {
        public Task CreateRelationshipAsync(DigitalTwinsClient client, string DigitalTwinsrcId, string DigitalTwintargetId);

        public void CreateTwins(String id, BasicDigitalTwin basicDigitalTwin);
        public BasicDigitalTwin CreateTwinContents(String modelId);


    }
}
