using Azure.DigitalTwins.Core;
using DigitalTvillingKlima.testfolder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTvillingKlima.Interface
{
    public interface IDigitalTwinsKlimaBuilder
    {
        
        public void CreateTwinsAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin);
        public BasicDigitalTwin CreateOmradeTwinContents(KlimaInfo klimaInfo, string idNavn, Coordinates coordinates);

        


    }
}
