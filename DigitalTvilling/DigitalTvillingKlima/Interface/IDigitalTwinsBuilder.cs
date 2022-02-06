using Azure.DigitalTwins.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Interface
{
    public interface IDigitalTwinsBuilder
    {
        
        public void CreateTwins(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin);
        public BasicDigitalTwin CreateTwinContents(String modelId);

        


    }
}
