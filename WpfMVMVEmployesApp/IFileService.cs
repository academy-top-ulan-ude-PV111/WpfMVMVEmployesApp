using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WpfMVMVEmployesApp
{
    public interface IFileService
    {
        List<Employe> Open(string path);
        void Save(string path, List<Employe> employes);
    }

    public class JsonFileService : IFileService
    {
        public List<Employe> Open(string path)
        {
            List<Employe> employes = new();
            DataContractJsonSerializer serializer = new(typeof(List<Employe>));
            using(FileStream file = new(path, FileMode.OpenOrCreate))
            {
                employes = serializer.ReadObject(file) as List<Employe>;
            }

            return employes;
        }

        public void Save(string path, List<Employe> employes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Employe>));
            using(FileStream file = new(path, FileMode.Create))
            {
                serializer.WriteObject(file, employes);
            }
        }
    }
}
