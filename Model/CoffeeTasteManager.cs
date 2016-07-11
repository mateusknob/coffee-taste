using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace CoffeeTaste.Model
{
    [Serializable()]
    public class CoffeeTasteManager
    {
        [XmlIgnore]
        public string fileName;
        public List<Rotulo> Rotulos { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }

        public CoffeeTasteManager()
        {
            fileName = AppDomain.CurrentDomain.BaseDirectory + "\\data.ser";

            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
            }
            Rotulos = new List<Rotulo>();
            Avaliacoes = new List<Avaliacao>();
        }

        public void Add(object obj)
        {
            if (obj is Rotulo)
            {
                Rotulos.Add((Rotulo)obj);
            }
            else if (obj is Avaliacao)
            {
                Avaliacoes.Add((Avaliacao)obj);
            }
            Persist();
        }

        public void Remove(object obj)
        {
            if (obj is Rotulo)
            {
                Rotulos.Remove((Rotulo)obj);
            }
            else if (obj is Avaliacao)
            {
                Avaliacoes.Remove((Avaliacao)obj);
            }
            Persist();
        }

        public CoffeeTasteManager GetData()
        {
            try
            {
                CoffeeTasteManager objectOut = default(CoffeeTasteManager);
                string attributeXml = string.Empty;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(CoffeeTasteManager);
                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (CoffeeTasteManager)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
                return objectOut;
            }
            catch
            {
                return new CoffeeTasteManager();
            }
        }

        private void Persist()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, this);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }
    }
}
