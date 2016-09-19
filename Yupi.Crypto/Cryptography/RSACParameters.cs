using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml.Serialization;
using Yupi.Crypto.Utils;

namespace Yupi.Crypto.Cryptography
{
    [XmlRoot(ElementName = "RSAKeyPair")]
    public class RSACParameters
    {
        private BigInteger _d;
        private BigInteger _dp;
        private BigInteger _dq;
        private BigInteger _exponent; // E
        private BigInteger _inverseQ;
        private BigInteger _modules; // N
        private BigInteger _p;
        private BigInteger _q;

        [XmlIgnore]
        public BigInteger D
        {
            get
            {
                return this._d;
            }
        }

        [XmlIgnore]
        public BigInteger DP
        {
            get
            {
                return this._dp;
            }
        }

        [XmlIgnore]
        public BigInteger DQ
        {
            get
            {
                return this._dq;
            }
        }

        [XmlIgnore]
        public BigInteger Exponent
        {
            get
            {
                return this._exponent;
            }
        }

        [XmlIgnore]
        public BigInteger InverseQ
        {
            get
            {
                return this._inverseQ;
            }
        }

        [XmlIgnore]
        public BigInteger Modules
        {
            get
            {
                return this._modules;
            }
        }

        [XmlIgnore]
        public BigInteger P
        {
            get
            {
                return this._p;
            }
        }

        [XmlIgnore]
        public BigInteger Q
        {
            get
            {
                return this._q;
            }
        }

        [XmlIgnore]
        public int ModulesBlockSize
        {
            get
            {
                return (this._modules.BitLength() + 7) / 8;
            }
        }

        [XmlIgnore]
        public bool HasCertKeys
        {
            get
            {
                return this._dp != 0 && this._dq != 0 && this._inverseQ != 0 && this._p != 0 && this._q != 0 && this.HasPrivateKeys;
            }
        }

        [XmlIgnore]
        public bool HasPublicKeys
        {
            get
            {
                return this._modules != 0 && this._exponent != 0;
            }
        }

        [XmlIgnore]
        public bool HasPrivateKeys
        {
            get
            {
                return this._d != 0 && this.HasPublicKeys;
            }
        }

        [XmlElement(ElementName = "D")]
        public string DB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._d, false);
            }
            set
            {
                this._d = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "DP")]
        public string DPB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._dp, false);
            }
            set
            {
                this._dp = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "DQ")]
        public string DQB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._dq, false);
            }
            set
            {
                this._dq = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "Exponent")]
        public string ExponentB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._exponent, false);
            }
            set
            {
                this._exponent = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "InverseQ")]
        public string InverseQB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._inverseQ, false);
            }
            set
            {
                this._inverseQ = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "Modules")]
        public string ModulesB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._modules, false);
            }
            set
            {
                this._modules = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "p")]
        public string PB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._p, false);
            }
            set
            {
                this._p = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        [XmlElement(ElementName = "Q")]
        public string QB64
        {
            get
            {
                return RSACUtils.BigIntegerToBase64(this._q, false);
            }
            set
            {
                this._q = RSACUtils.Base64ToBigInteger(value, true);
            }
        }

        public RSACParameters(BigInteger d, BigInteger dp, BigInteger dq, BigInteger exponent, BigInteger inverseQ, BigInteger modules, BigInteger p, BigInteger q)
        {
            this._d = d;
            this._dp = dp;
            this._dq = dq;
            this._exponent = exponent;
            this._inverseQ = inverseQ;
            this._modules = modules;
            this._p = p;
            this._q = q;
        }

        public RSACParameters(BigInteger modules, BigInteger exponent)
            : this(0, modules, exponent)
        { }

        public RSACParameters(BigInteger d, BigInteger modules, BigInteger exponent)
            : this(d, 0, 0, exponent, 0, modules, 0, 0)
        { }

        public RSACParameters()
            : this(0, 0, 0, 0, 0, 0, 0, 0)
        { }


        public static RSACParameters FromXmlString(string xmlString)
        {
            RSACParameters result = null;

            using (TextReader reader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RSACParameters));

                try
                {
                    result = (RSACParameters)serializer.Deserialize(reader);
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidDataException("Provided XML data is not a valid RSA Parameter format! " + e.Message);
                }
            }

            return result;
        }

        public static RSACParameters FromXmlFile(string xmlFile)
        {
            string xmlString = File.ReadAllText(xmlFile);

            return FromXmlString(xmlString);
        }

        public string ToXmlString()
        {
            StringBuilder result = new StringBuilder();
            using (TextWriter writer = new StringWriter(result))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(RSACParameters));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");

                serializer.Serialize(writer, this, namespaces);
            }

            return result.ToString();
        }

        public void ToXmlFile(string xmlFile)
        {
            string xmlString = this.ToXmlString();

            File.WriteAllText(xmlFile, xmlString);
        }
    }
}
