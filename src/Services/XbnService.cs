using BlubLib.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XbnConvert
{
    public class XbnService
    {
        // private static readonly Encoding Encoding = CodePagesEncodingProvider.Instance.GetEncoding(1252);
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);

        private readonly Dictionary<XBNType, byte[]> _cache;
        private byte[] _xbnBytes;

        public XbnService()
        {
            _cache = new Dictionary<XBNType, byte[]>();
        }

        public byte[] GetData(XBNType xbnType)
        {
            _cache.TryGetValue(xbnType, out var byte_s);
            return byte_s;
        }

        public IReadOnlyDictionary<XBNType, byte[]> GetData()
        {
            return _cache;
        }

        #region #DefaultXbnConvert
        public Task StartAsync()
        {
            // s_logger.Information("Generating xbn files...");

            foreach (XBNType xbnType in Enum.GetValues(typeof(XBNType)))
            {
                string fileName;
                switch (xbnType)
                {
                    case XBNType.ConstantInfo:
                        fileName = "xml/ConstantInfo.xml";
                        break;

                    case XBNType.Actions:
                        fileName = "xml/Actions.xml";
                        break;

                    case XBNType.Weapons:
                        fileName = "xml/Weapons.xml";
                        break;

                    case XBNType.Effects:
                        fileName = "xml/Effects.xml";
                        break;

                    case XBNType.EffectMatch:
                        fileName = "xml/EffectMatch.xml";
                        break;

                    case XBNType.EnchantData:
                        fileName = "xml/EnchantData.xml";
                        break;

                    case XBNType.EquipLimit:
                        fileName = "xml/EquipLimit.xml";
                        break;

                    case XBNType.PveList:
                        fileName = "xml/PveList.xml";
                        break;

                    case XBNType.MonsterWaveList:
                        fileName = "xml/MonsterWaveList.xml";
                        break;

                    case XBNType.EsperActionlist:
                        fileName = "xml/EsperActionlist.xml";
                        break;

                    case XBNType.WeaponTypeReloadInfo:
                        fileName = "xml/WeaponTypeReloadInfo.xml";
                        break;

                    case XBNType.RangeLimit:
                        fileName = "xml/RangeLimit.xml";
                        break;

                    default:
                        throw new Exception("Invalid xbn type: " + xbnType);
                }

                using (var xmlReader = new XmlTextReader(fileName))
                {
                    var xml = XDocument.Load(xmlReader);

                    using (var w = new MemoryStream().ToBinaryWriter(false))
                    {
                        w.Write(-1);
                        w.Write(0.1f);

                        if (xml.Root == null)
                        {
                            w.Write((ushort)0);
                            w.Write((ushort)0);
                        }
                        else
                        {
                            var nodeNames = GetNodeNames(xml.Root);
                            w.Write((ushort)nodeNames.Count);
                            foreach (var nodeName in nodeNames)
                                WriteXBNValue(w, nodeName);

                            var attribNames = GetAttributeNames(xml.Root);
                            w.Write((ushort)attribNames.Count);
                            foreach (var attribName in attribNames)
                                WriteXBNValue(w, attribName);

                            WriteXBNNode(w, xml.Root, nodeNames, attribNames);
                        }

                        _cache[xbnType] = w.ToArray();
                    }
                }
            }

            // foreach (var i in _cache)
            // {
            //    File.WriteAllBytes($"xml/{i.Key}.xbn", i.Value);
            // }

            // s_logger.Information("Cached {Count} xbn files...", _cache.Count);
            return Task.CompletedTask;

            IList<string> GetNodeNames(XElement element)
            {
                var names = new List<string> { element.Name.LocalName };
                foreach (var subNode in element.Elements())
                    names.AddRange(GetNodeNames(subNode).Where(e => !names.Contains(e)));

                return names;
            }

            IList<string> GetAttributeNames(XElement element)
            {
                var names = element.Attributes().Select(attrib => attrib.Name.LocalName).ToList();
                foreach (var subNode in element.Elements())
                    names.AddRange(GetAttributeNames(subNode).Where(e => !names.Contains(e)));

                return names;
            }
        }
        #endregion

        #region #PersonalXbnConvert
        public Task StartAsync(string fileName)
        {
            var file = Path.GetFileNameWithoutExtension(fileName);
            var path = Path.GetDirectoryName(fileName);

            using (var xmlReader = new XmlTextReader(fileName))
            {
                var xml = XDocument.Load(xmlReader);

                using (var w = new MemoryStream().ToBinaryWriter(false))
                {
                    w.Write(-1);
                    w.Write(0.1f);

                    if (xml.Root == null)
                    {
                        w.Write((ushort)0);
                        w.Write((ushort)0);
                    }
                    else
                    {
                        var nodeNames = GetNodeNames(xml.Root);
                        w.Write((ushort)nodeNames.Count);
                        foreach (var nodeName in nodeNames)
                            WriteXBNValue(w, nodeName);

                        var attribNames = GetAttributeNames(xml.Root);
                        w.Write((ushort)attribNames.Count);
                        foreach (var attribName in attribNames)
                            WriteXBNValue(w, attribName);

                        WriteXBNNode(w, xml.Root, nodeNames, attribNames);
                    }

                    _xbnBytes = w.ToArray();
                }
            }

            var xbnfile = GenerateXbnFile(Path.Combine(path, file));
            var xbnFileName = Path.GetFileNameWithoutExtension(xbnfile) + ".xbn";
            File.WriteAllBytes(xbnfile, _xbnBytes);
            // NotifyDesktop.ShowNotify(new IWriteLogger("Conversión exitosa: {0}", xbnFileName), "Éxito!");
            return Task.CompletedTask;

            IList<string> GetNodeNames(XElement element)
            {
                var names = new List<string> { element.Name.LocalName };
                foreach (var subNode in element.Elements())
                    names.AddRange(GetNodeNames(subNode).Where(e => !names.Contains(e)));

                return names;
            }

            IList<string> GetAttributeNames(XElement element)
            {
                var names = element.Attributes().Select(attrib => attrib.Name.LocalName).ToList();
                foreach (var subNode in element.Elements())
                    names.AddRange(GetAttributeNames(subNode).Where(e => !names.Contains(e)));

                return names;
            }
        }
        #endregion

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }

        private static void WriteXBNValue(BinaryWriter w, string value)
        {
            var data = Encoding.GetBytes(value);

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte)((data[i] >> 6) | (data[i] << 2));
                data[i] = (byte)~data[i];
            }

            w.Write((ushort)data.Length);
            w.Write(data);
        }

        private static void WriteXBNAttribute(BinaryWriter w, XAttribute attribute, IList<string> attribNames)
        {
            var nameIndex = attribNames.IndexOf(attribute.Name.LocalName);
            w.Write((ushort)nameIndex);
            WriteXBNValue(w, attribute.Value);
        }

        private static void WriteXBNNode(BinaryWriter w, XElement node, IList<string> nodeNames, IList<string> attribNames)
        {
            var nameIndex = nodeNames.IndexOf(node.Name.LocalName);

            w.Write((ushort)nameIndex);

            w.Write((ushort)node.Attributes().Count());
            w.Write((ushort)0); // TODO unk
            w.Write((ushort)node.Elements().Count());

            foreach (var attrib in node.Attributes())
                WriteXBNAttribute(w, attrib, attribNames);

            foreach (var subNode in node.Elements())
                WriteXBNNode(w, subNode, nodeNames, attribNames);
        }

        private string GenerateXbnFile(string file)
        {
            int value = 0;
            string xbn_file;
            while (true)
            {
                xbn_file = file + value.ToString().Replace("0", "") + ".xbn";
                if (!File.Exists(xbn_file))
                    break;
                value++;
            }
            return xbn_file;
        }
    }
}
