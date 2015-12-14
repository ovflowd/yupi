namespace Yupi.Game.Catalogs.Interfaces
{
    internal class TargetedOffer
    {
        internal uint CostCredits, CostDuckets, CostDiamonds;
        internal int ExpirationTime;
        internal int Id;
        internal string Identifier;
        internal string[] Products;
        internal int PurchaseLimit;
        internal string Title, Description, Image;

        public TargetedOffer(int id, string identifier, uint costCredits, uint costDuckets, uint costDiamonds,
            int purchaseLimit, int expirationTime, string title, string description, string image, string products)
        {
            Id = id;
            Identifier = identifier;
            CostCredits = costCredits;
            CostDuckets = costDuckets;
            CostDiamonds = costDiamonds;
            PurchaseLimit = purchaseLimit;
            ExpirationTime = expirationTime;
            Title = title;
            Description = description;
            Image = image;
            Products = products.Split(';');
        }
    }
}