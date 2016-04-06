namespace Yupi.Emulator.Game.Catalogs.Interfaces
{
     public class TargetedOffer
    {
     public uint CostCredits, CostDuckets, CostDiamonds;
     public int ExpirationTime;
     public int Id;
     public string Identifier;
     public string[] Products;
     public int PurchaseLimit;
     public string Title, Description, Image;

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