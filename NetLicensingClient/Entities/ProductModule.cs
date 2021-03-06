using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Represents ProductModule. See NetLicensingAPI JavaDoc for details:
    /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/ProductModule.html
    /// </summary>
    public class ProductModule : BaseEntity
    {
        /// <summary>
        /// ProductModule name. Not null. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/ProductModule.html
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// LicensingModel name. Not null. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/ProductModule.html
        /// </summary>
        public String licensingModel { get; set; }

        /// <summary>
        /// Product number related to this ProductModule. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/ProductModule.html
        /// </summary>
        public String productNumber { get; set; }

        /// <summary>
        /// Custom properties of the product module. See NetLicensingAPI JavaDoc for details:
        /// https://go.netlicensing.io/javadoc/v2/com/labs64/netlicensing/domain/entity/ProductModule.html
        /// </summary>
        public Dictionary<String, String> productModuleProperties { get; private set; }

        // default constructor
        public ProductModule()
        {
            productModuleProperties = new Dictionary<String, String>();
        }

        // construct from REST response item
        internal ProductModule(item source)
        {
            if (!Constants.ProductModule.TYPE_NAME.Equals(source.type))
            {
                throw new NetLicensingException(String.Format("Wrong object type '{0}', expected '{1}'", (source.type != null) ? source.type : "<null>", Constants.ProductModule.TYPE_NAME));
            }
            productModuleProperties = new Dictionary<String, String>();
            foreach (property p in source.property)
            {
                switch (p.name)
                {
                    case Constants.NAME:
                        name = p.Value;
                        break;
                    case Constants.ProductModule.PRODUCT_MODULE_LICENSING_MODEL:
                        licensingModel = p.Value;
                        break;
                    case Constants.Product.PRODUCT_NUMBER:
                        productNumber = p.Value;
                        break;
                    default:
                        if (!base.setFromProperty(p)) // Not BaseEntity property?
                        {
                            // custom property
                            if (!productModuleProperties.ContainsKey(p.name))
                            {
                                productModuleProperties.Add(p.name, p.Value);
                            }
                        }
                        break;
                }
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Constants.ProductModule.TYPE_NAME);
            sb.Append("[");
            sb.Append(base.ToString());
            sb.Append(", ");
            sb.Append(Constants.Product.PRODUCT_NUMBER);
            sb.Append("=");
            sb.Append(productNumber);
            sb.Append(", ");
            sb.Append(Constants.NAME);
            sb.Append("=");
            sb.Append(name);
            sb.Append(", ");
            sb.Append(Constants.ProductModule.PRODUCT_MODULE_LICENSING_MODEL);
            sb.Append("=");
            sb.Append(licensingModel);
            foreach (KeyValuePair<String, String> prop in productModuleProperties)
            {
                sb.Append(", ");
                sb.Append(prop.Key);
                sb.Append("=");
                sb.Append(prop.Value);
            }
            sb.Append("]");
            return sb.ToString();
        }

        internal new Dictionary<String, String> ToDictionary()
        {
            Dictionary<String, String> dict = base.ToDictionary();
            if (name != null) dict[Constants.NAME] = name;
            if (licensingModel != null) dict[Constants.ProductModule.PRODUCT_MODULE_LICENSING_MODEL] = licensingModel;
            if (productNumber != null) dict[Constants.Product.PRODUCT_NUMBER] = productNumber;
            foreach (KeyValuePair<String, String> prop in productModuleProperties)
            {
                dict[prop.Key] = prop.Value;
            }
            return dict;
        }

    }
}
