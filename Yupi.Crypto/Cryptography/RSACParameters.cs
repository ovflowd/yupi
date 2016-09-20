namespace Yupi.Crypto.Cryptography
{
    using System;
    using System.IO;
    using System.Numerics;
    using System.Text;
    using System.Xml.Serialization;

    using Yupi.Crypto.Utils;

    [XmlRoot(ElementName = "RSAKeyPair")]
    public class RSACParameters
    {
        #region Fields

        private BigInteger _d;
        private BigInteger _dp;
        private BigInteger _dq;
        private BigInteger _exponent; // E
        private BigInteger _inverseQ;
        private BigInteger _modules; // N
        private BigInteger _p;
        private BigInteger _q;

        #endregion Fields

        #region Properties

        [XmlIgnore]
        public BigInteger D
        {
            get
            {
                return this._d;
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

        [XmlIgnore]
        public BigInteger DP
        {
            get
            {
                return this._dp;
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

        [XmlIgnore]
        public BigInteger DQ
        {
            get
            {
                return this._dq;
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

        [XmlIgnore]
        public BigInteger Exponent
        {
            get
            {
                return this._exponent;
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

        [XmlIgnore]
        public bool HasCertKeys
        {
            get
            {
                return this._dp != 0 && this._dq != 0 && this._inverseQ != 0 && this._p != 0 && this._q != 0 && this.HasPrivateKeys;
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

        [XmlIgnore]
        public bool HasPublicKeys
        {
            get
            {
                return this._modules != 0 && this._exponent != 0;
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

        [XmlIgnore]
        public BigInteger Modules
        {
            get
            {
                return this._modules;
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

        [XmlIgnore]
        public int ModulesBlockSize
        {
            get
            {
                return (this._modules.BitLength() + 7) / 8;
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

        [XmlIgnore]
        public BigInteger Q
        {
            get
            {
                return this._q;
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

        #endregion Properties

        #region Constructors

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
        {
        }

        public RSACParameters(BigInteger d, BigInteger modules, BigInteger exponent)
            : this(d, 0, 0, exponent, 0, modules, 0, 0)
        {
        }

        public RSACParameters()
            : this(0, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        #endregion Constructors

        #region Methods

        public static RSACParameters FromXmlFile(string xmlFile)
        {
            string xmlString = File.ReadAllText(xmlFile);

            return FromXmlString(xmlString);
        }

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

        public void ToXmlFile(string xmlFile)
        {
            string xmlString = this.ToXmlString();

            File.WriteAllText(xmlFile, xmlString);
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

        #endregion Methods
    }
}