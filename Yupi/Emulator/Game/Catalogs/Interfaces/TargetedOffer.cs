namespace Yupi.Emulator.Game.Catalogs.Interfaces
{
     class TargetedOffer
    {
         uint CostCredits, CostDuckets, CostDiamonds;
         int ExpirationTime;
         int Id;
         string Identifier;
         string[] Products;
         int PurchaseLimit;
         string Title, Description, Image;

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