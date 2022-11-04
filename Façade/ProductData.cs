namespace PatternsUncleBob.Façade
{
    public class ProductData
    {
        public string name;
        public int price;
        public string sku;

        public ProductData(string name, int price, string sku)
        {
            this.name = name;
            this.price = price;
            this.sku = sku;
        }

        public ProductData()
        {

        }

        public override bool Equals(object obj)
        {
            ProductData pd = (ProductData)obj;
            return name.Equals(pd.name) && sku.Equals(pd.sku) && price == pd.price;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ sku.GetHashCode() ^ price.GetHashCode();
        }
    }
}