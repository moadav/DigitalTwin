using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Interface
{
    public interface IDigitalTwinsBuilder
    {
        
        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin);
        public BasicDigitalTwin CreateKlimaTwinContents(String modelId, KlimaInfo klimaInfo, string navn, string idNavn);

        


    }
}
