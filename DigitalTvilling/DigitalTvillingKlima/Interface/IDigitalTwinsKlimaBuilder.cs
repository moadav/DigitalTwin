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


        /// <summary>Creates the twin asynchronous.</summary>
        /// <param name="client">The client. <see cref="DigitalTwinsClient"/></param>
        /// <param name="basicDigitalTwin">The BasicDigitalTwin<see cref="BasicDigitalTwin"/></param>
        public void CreateTwinAsync(DigitalTwinsClient client, BasicDigitalTwin basicDigitalTwin);


        /// <summary>Creates the omrade twin contents.</summary>
        /// <param name="klimaInfo">KlimaInfo class<see cref="KlimaInfo"/></param>
        /// <param name="idNavn">The identifier navn.</param>
        /// <param name="coordinates">the Coordinates class<see cref="Coordinates"/></param>
        /// <returns>a BasicDigitalTwin</returns>
        public BasicDigitalTwin CreateOmradeTwinContents(KlimaInfo klimaInfo, string idNavn, Coordinates coordinates);

    }
}
