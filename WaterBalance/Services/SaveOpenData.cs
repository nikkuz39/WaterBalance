using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using WaterBalance.Models;

namespace WaterBalance.Services
{
    internal sealed class SaveOpenData
    {
        public List<Consumer> OpenJsonFile(string fileName)
        {
            List<Consumer> consumers = new List<Consumer>();

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Consumer>));

            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                consumers = jsonSerializer.ReadObject(stream) as List<Consumer>;
            }
            return consumers;
        }

        public void SaveJsonFile(string fileName, List<Consumer> consumers)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Consumer>));

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                jsonSerializer.WriteObject(stream, consumers);
            }
        }
    }
}
