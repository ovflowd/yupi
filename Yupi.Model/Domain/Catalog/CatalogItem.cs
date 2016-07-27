using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Yupi.Model.Domain
{
	public class CatalogItem
	{
		public virtual int Id { get; protected set; }
		public virtual bool ClubOnly { get; protected set; }
		public virtual int CreditsCost { get; protected set; }
		public virtual int DiamondsCost { get; protected set; }
		public virtual int DucketsCost { get; protected set; }
		public virtual bool HaveOffer { get; protected set; }
		public virtual string Name { get; protected set; }
		public virtual string Badge { get; protected set; }

		public virtual BaseItem BaseItem { get; protected set; }
	}
}